using System;
using System.Collections.Generic;
using UnityEngine;
using ZeepSDK.LevelEditor;
using Object = UnityEngine.Object;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal static class TrailManager
{
    private static readonly List<GameObject> _trailContainers = [];
    public static List<Trail> LoadedTrails { get; private set; } = [];

    static TrailManager()
    {
        PluginConfig.ColorMode.SettingChanged += OnColorModeChanged;
    }

    private static void OnColorModeChanged(object sender, EventArgs e)
    {
        if (LevelEditorApi.IsInLevelEditor)
        {
            DestroyTrailRenderers();
            CreateTrailRenderers();
        }
    }

    public static void CreateTrailRenderers()
    {
        if (PluginConfig.ColorMode.Value == TrailColorMode.Single)
        {
            foreach (Trail trail in LoadedTrails)
            {
                CreateTrail(trail, PluginConfig.Color.Value);
            }
        }
        else
        {
            if (LoadedTrails.Count == 1)
            {
                CreateTrail(LoadedTrails[0], PluginConfig.StartColor.Value);
            }
            else
            {
                for (int i = 0; i < LoadedTrails.Count; i++)
                {
                    CreateTrail(LoadedTrails[i],
                        Color.Lerp(PluginConfig.StartColor.Value, PluginConfig.EndColor.Value,
                            Remap(i, 0, LoadedTrails.Count - 1, 0f, 1f)));
                }    
            }
        }
    }

    private static void CreateTrail(Trail trail, Color color)
    {
        GameObject trailContainer = new("Trail");
        GameObject trailRendererContainer = new("TrailRenderer");
        trailRendererContainer.transform.SetParent(trailContainer.transform);
        TrailRenderer trailRenderer = trailRendererContainer.AddComponent<TrailRenderer>();
        trailRenderer.Initialize(trail, color);
        GameObject trailTimeMarkerContainer = new("TrailTimeMarker");
        trailTimeMarkerContainer.transform.SetParent(trailContainer.transform);
        TrailTimeMarkerRenderer timeMarkerRenderer = trailTimeMarkerContainer.AddComponent<TrailTimeMarkerRenderer>();
        timeMarkerRenderer.Initialize(trail);
        _trailContainers.Add(trailContainer);
    }

    private static float Remap(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }

    public static void DestroyTrailRenderers()
    {
        foreach (GameObject trailContainer in _trailContainers)
        {
            Object.Destroy(trailContainer);
        }
        
        _trailContainers.Clear();
    }

    public static void LoadTrails(List<Trail> trails)
    {
        DestroyTrailRenderers();
        LoadedTrails.Clear();
        LoadedTrails.AddRange(trails);
    }

    public static void AddTrail(Trail trail)
    {
        LoadedTrails.Add(trail);
        
        // This should only be needed once since we cannot go over
        if (LoadedTrails.Count > PluginConfig.MaxRecordings.Value)
        {
            LoadedTrails.RemoveAt(0);
        }

        TrailStorage.SaveForCurrentLevel(LoadedTrails);
    }

    public static void HideTrails()
    {
        foreach (GameObject trailContainer in _trailContainers)
        {
            trailContainer.SetActive(false);
        }
    }

    public static void ShowTrails()
    {
        foreach (GameObject trailContainer in _trailContainers)
        {
            trailContainer.SetActive(true);
        }
    }
}