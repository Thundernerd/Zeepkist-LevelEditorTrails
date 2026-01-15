using System;
using BepInEx.Configuration;
using UnityEngine;
using ZeepSDK.LevelEditor;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class PluginConfig : MonoBehaviour
{
    private Plugin _plugin;

    private bool _isInEditor;
    private bool _isTesting;

    private ConfigFile Config => _plugin.Config;

    public static event Action RemoveLinesPressed;
    
    public static ConfigEntry<int> LevelOfDetail { get; private set; }
    public static ConfigEntry<int> MaxRecordings { get; private set; }
    public static ConfigEntry<float> LineWidth { get; private set; }
    
    public static ConfigEntry<bool> LinesVisible { get; private set; }
    public static ConfigEntry<bool> TimeMarkersVisible { get; private set; }
    public static ConfigEntry<float> TimeMarkerStep { get; private set; }

    public static ConfigEntry<KeyCode> KeyToggleLineVisibility { get; private set; }
    public static ConfigEntry<KeyCode> KeyToggleTimeMarkerVisibility { get; private set; }
    public static ConfigEntry<KeyCode> KeyRemoveLines { get; private set; }
    
    public static ConfigEntry<TrailColorMode> ColorMode { get; private set; }
    public static ConfigEntry<Color> Color { get; private set; }
    public static ConfigEntry<Color> StartColor { get; private set; }
    public static ConfigEntry<Color> EndColor { get; private set; }

    private void Awake()
    {
        LevelEditorApi.EnteredLevelEditor += OnEnteredLevelEditor;
        LevelEditorApi.ExitedLevelEditor += OnExitedLevelEditor;
        LevelEditorApi.EnteredTestMode += OnEnteredTestMode;
    }

    private void OnDestroy()
    {
        LevelEditorApi.EnteredLevelEditor -= OnEnteredLevelEditor;
        LevelEditorApi.ExitedLevelEditor -= OnExitedLevelEditor;
        LevelEditorApi.EnteredTestMode -= OnEnteredTestMode;
    }

    private void OnEnteredLevelEditor()
    {
        _isInEditor = true;
        _isTesting = false;
    }

    private void OnExitedLevelEditor()
    {
        _isInEditor = false;
    }

    private void OnEnteredTestMode()
    {
        _isTesting = true;
    }

    public void Initialize(Plugin plugin)
    {
        _plugin = plugin;
        
        BindSettings();
    }

    private void BindSettings()
    {
        LevelOfDetail = Config.Bind("General",
            "levelOfDetail",
            5,
            new ConfigDescription(
                $"Min = {Constants.MIN_LOD} / Max = {Constants.MAX_LOD}. The higher the more detailed",
                new AcceptableValueRange<int>(Constants.MIN_LOD, Constants.MAX_LOD)));
        
        MaxRecordings = Config.Bind("General",
            "Maximum Recordings",
            5,
            "How many trails should we keep");

        LineWidth = Config.Bind("General",
            "Trail Width",
            0.5f,
            new ConfigDescription(
                "The width of the trail line",
                new AcceptableValueRange<float>(Constants.MIN_WIDTH, Constants.MAX_WIDTH)));

        LinesVisible = Config.Bind("General",
            "linesVisible",
            true,
            new ConfigDescription("[Hidden]"));

        TimeMarkersVisible = Config.Bind("General",
            "Draw Time Markers",
            true,
            new ConfigDescription("Draw a marker every X seconds as specified by Time Marker Step"));

        TimeMarkerStep = Config.Bind("General",
            "Time Marker Step",
            1f,
            new ConfigDescription("The time in seconds between every time marker"));

        KeyToggleLineVisibility = Config.Bind("Keys",
            "toggleVisibility",
            KeyCode.F7,
            "Toggles the visibility of the lines");

        KeyRemoveLines = Config.Bind("Keys",
            "removeAllLines",
            KeyCode.F8,
            "Pressing this will remove all lines");

        KeyToggleTimeMarkerVisibility = Config.Bind("Keys",
            "Toggle Time Marker Visibility",
            KeyCode.F6,
            "Toggles the visibility of the time markers");

        ColorMode = Config.Bind("Colors",
            "Color Mode",
            TrailColorMode.Gradient,
            "The mode for the colors of the trails");

        Color = Config.Bind("Colors",
            "Color",
            UnityEngine.Color.white,
            "The color used in the single color mode");
        
        StartColor = Config.Bind("Colors",
            "Start Color",
            UnityEngine.Color.white,
            "The color for the newest trail used in the gradient color mode");

        EndColor = Config.Bind("Colors",
            "End Color",
            UnityEngine.Color.black,
            "The color for the oldest trail used in the gradient color mode");
    }

    private void Update()
    {
        if (!_isInEditor && !_isTesting)
            return;

        if (Input.GetKeyDown(KeyToggleLineVisibility.Value))
        {
            LinesVisible.Value = !LinesVisible.Value;
        }

        if (Input.GetKeyDown(KeyToggleTimeMarkerVisibility.Value))
        {
            TimeMarkersVisible.Value = !TimeMarkersVisible.Value;
        }

        if (Input.GetKeyDown(KeyRemoveLines.Value))
        {
            RemoveLinesPressed?.Invoke();
        }
    }
}