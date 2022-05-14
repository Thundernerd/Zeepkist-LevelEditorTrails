using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Zeepkist.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public static bool ShouldBeVisible { get; private set; } = true;

        public static ConfigEntry<int> LevelOfDetail { get; private set; }
        public static ConfigEntry<int> MaxRecordings { get; private set; }
        public static ConfigEntry<float> LineWidth { get; private set; }

        public static ConfigEntry<KeyCode> KeyToggleVisibility { get; private set; }
        public static ConfigEntry<KeyCode> KeyRemoveLines { get; private set; }

        private Harmony harmony;

        private void Awake()
        {
            LevelOfDetail = Config.Bind("General",
                "levelOfDetail",
                5,
                new ConfigDescription(
                    $"Min = {Constants.MIN_LOD} / Max = {Constants.MAX_LOD}. The higher the more detailed",
                    new AcceptableValueRange<int>(Constants.MIN_LOD, Constants.MAX_LOD)));

            MaxRecordings = Config.Bind("General",
                "maxRecordings",
                5,
                "How many trails should we keep");

            LineWidth = Config.Bind("General",
                "lineWidth",
                0.5f,
                new ConfigDescription(
                    $"Min = {Constants.MIN_WIDTH} / Max = {Constants.MAX_WIDTH}. The higher the thicker",
                    new AcceptableValueRange<float>(Constants.MIN_WIDTH, Constants.MAX_WIDTH)));

            KeyToggleVisibility = Config.Bind("Keys",
                "toggleVisibility",
                KeyCode.F7,
                "Toggles the visibilty of the lines");

            KeyRemoveLines = Config.Bind("Keys",
                "removeAllLines",
                KeyCode.F8,
                "Pressing this will remove all lines");

            harmony = new Harmony("net.tnrd.zeepkist.trailrenderer");
            harmony.PatchAll();

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyToggleVisibility.Value))
            {
                ShouldBeVisible = !ShouldBeVisible;
            }
            else if (Input.GetKeyDown(KeyRemoveLines.Value))
            {
                RecordedTrails.Clear();
            }
        }
    }
}
