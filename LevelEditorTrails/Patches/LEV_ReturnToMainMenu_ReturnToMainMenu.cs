using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(LEV_ReturnToMainMenu), nameof(LEV_ReturnToMainMenu.ReturnToMainMenu))]
public class LEV_ReturnToMainMenu_ReturnToMainMenu
{
    public static bool IsLeavingToMainMenu { get; private set; }

    private static void Prefix(LEV_ReturnToMainMenu __instance, out bool __state)
    {
        IsLeavingToMainMenu = false;

        __state = __instance.central.manager.unsavedContent;
    }

    private static void Postfix(LEV_ReturnToMainMenu __instance, bool __state)
    {
        if (!__state)
        {
            IsLeavingToMainMenu = true;
            return;
        }

        __instance.central.unsavedContentPopup.ShouldQuit.AddListener(quit => { IsLeavingToMainMenu = quit; });
    }
}
