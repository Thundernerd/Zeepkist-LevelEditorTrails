using System;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class TrailTimeMarkerRenderer : MonoBehaviour
{
    private Trail _trail;
    
    private void Awake()
    {
        PluginConfig.TimeMarkersVisible.SettingChanged += OnVisibleChanged;
        PluginConfig.TimeMarkerStep.SettingChanged += OnStepChanged;
    }

    private void OnDestroy()
    {
        PluginConfig.TimeMarkersVisible.SettingChanged -= OnVisibleChanged;
        PluginConfig.TimeMarkerStep.SettingChanged -= OnStepChanged;
    }

    private void OnVisibleChanged(object sender, EventArgs e)
    {
        gameObject.SetActive(PluginConfig.TimeMarkersVisible.Value);
    }

    public void Initialize(Trail trail)
    {
        _trail = trail;
        CreateMarkers();
        gameObject.SetActive(PluginConfig.TimeMarkersVisible.Value);
    }

    private void DestroyMarkers()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void CreateMarkers()
    {
        float previousMod = 0;

        foreach (TrailFrame frame in _trail.Frames)
        {
            float currentMod = frame.Time % PluginConfig.TimeMarkerStep.Value;

            if (currentMod < previousMod)
            {
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                primitive.transform.SetParent(transform);
                primitive.transform.position = frame.Position;
                primitive.transform.localScale = Vector3.one *
                                                 (PluginConfig.LineWidth.Value /
                                                  (float)PluginConfig.LineWidth.DefaultValue);
                if (primitive.TryGetComponent(out Collider collider))
                {
                    Destroy(collider);
                }
            }

            previousMod = currentMod;
        }
    }

    private void OnStepChanged(object sender, EventArgs e)
    {
        DestroyMarkers();
        CreateMarkers();
    }
}