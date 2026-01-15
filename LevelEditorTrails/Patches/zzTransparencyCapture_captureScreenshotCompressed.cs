using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(zzTransparencyCapture), nameof(zzTransparencyCapture.captureScreenshotCompressed))]
internal class zzTransparencyCapture_captureScreenshotCompressed
{
    private static void Postfix()
    {
        TrailManager.ShowTrails();
    }
}