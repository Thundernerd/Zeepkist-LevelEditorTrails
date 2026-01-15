using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(LEV_ScreenshotMaker), nameof(LEV_ScreenshotMaker.Screenshot))]
internal class LEV_ScreenshotMaker_Screenshot
{
    public static void Prefix()
    {
        TrailManager.HideTrails();
    }
}