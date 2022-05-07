using HarmonyLib;
using JetBrains.Annotations;

namespace TrailRendererLevelEditor.Patches
{
    [HarmonyPatch(typeof(LEV_PauseMenu), nameof(LEV_PauseMenu.QuitToMenu))]
    public class LEV_PauseMenuPatch
    {
        [UsedImplicitly]
        internal static void Postfix()
        {
            RecordedTrails.Clear();
            ColorManager.ResetColors();
        }
    }
}
