using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class TrailRenderer : MonoBehaviour
{
    private LineRenderer _line;

    private void Awake()
    {
        _line = gameObject.AddComponent<LineRenderer>();
        _line.material = new Material(Shader.Find("Sprites/Default"));

        _line.widthMultiplier = Mathf.Clamp(PluginConfig.LineWidth.Value, Constants.MIN_WIDTH, Constants.MAX_WIDTH);

        _line.numCapVertices = 5;
        _line.numCornerVertices = 5;

        _line.receiveShadows = false;
        _line.shadowCastingMode = ShadowCastingMode.Off;
        _line.loop = false;
        
        PluginConfig.LinesVisible.SettingChanged += OnLinesVisibleChanged;
    }

    private void OnDestroy()
    {
        PluginConfig.LinesVisible.SettingChanged -= OnLinesVisibleChanged;
    }

    private void OnLinesVisibleChanged(object sender, EventArgs e)
    {
        gameObject.SetActive(PluginConfig.LinesVisible.Value);
    }

    public void Initialize(Trail trail, Color color)
    {
        _line.startColor = color;
        _line.endColor = color;

        var step = PluginConfig.LineFidelity.Value;
        var positions = trail.Frames
            .Where((_, i) => i == 0 || i == trail.Frames.Count - 1 || i % step == 0)
            .Select(x => x.Position)
            .ToArray();

        _line.positionCount = positions.Length;
        _line.SetPositions(positions);

        gameObject.SetActive(PluginConfig.LinesVisible.Value);
    }
}