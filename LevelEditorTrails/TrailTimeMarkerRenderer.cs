using System;
using System.Collections.Generic;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class TrailTimeMarkerRenderer : MonoBehaviour
{
    private Trail _trail;
    private readonly List<List<Matrix4x4>> _matrixGroups = [];
    private Mesh _mesh;
    private Material _material;
    
    private void Awake()
    {
        PluginConfig.TimeMarkerStep.SettingChanged += OnStepChanged;
    }

    private void OnDestroy()
    {
        PluginConfig.TimeMarkerStep.SettingChanged -= OnStepChanged;
    }

    public void Initialize(Trail trail)
    {
        _trail = trail;
        CreateMarkers();
        
        
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        if (_mesh == null)
        {
            _mesh = obj.GetComponent<MeshFilter>().mesh;
        }

        if (_material == null)
        {
            _material = obj.GetComponent<MeshRenderer>().material;
            _material.enableInstancing = true;
        }

        Destroy(obj);
    }

    private void Update()
    {
        if (!PluginConfig.TimeMarkersVisible.Value)
            return;

        foreach (List<Matrix4x4> matrixGroup in _matrixGroups)
        {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, matrixGroup);
        }
    }

    private void CreateMarkers()
    {
        float previousMod = 0;

        List<Matrix4x4> group = new(1023);
        
        foreach (TrailFrame frame in _trail.Frames)
        {
            float currentMod = frame.Time % PluginConfig.TimeMarkerStep.Value;

            if (currentMod < previousMod)
            {
                group.Add(Matrix4x4.TRS(
                    frame.Position,
                    Quaternion.identity,
                    Vector3.one * (PluginConfig.LineWidth.Value /
                                   (float)PluginConfig.LineWidth.DefaultValue)));

                if (group.Count == 1023)
                {
                    _matrixGroups.Add(group);
                    group.Clear();
                }
            }

            previousMod = currentMod;
        }

        if (group.Count > 0)
        {
            _matrixGroups.Add(group);
        }
    }

    private void OnStepChanged(object sender, EventArgs e)
    {
        CreateMarkers();
    }
}