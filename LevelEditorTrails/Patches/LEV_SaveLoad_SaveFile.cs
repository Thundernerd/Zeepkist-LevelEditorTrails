using System;
using System.IO;
using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(LEV_SaveLoad),nameof(LEV_SaveLoad.SaveFile))]
internal class LEV_SaveLoad_SaveFile
{
    public static event Action<string> Postfixed;

    [HarmonyPrefix]
    public static void Prefix(LEV_SaveLoad __instance, out FileInfo __state)
    {
        __state = __instance.GetLevelWeJustSaved();
    }
    
    [HarmonyPostfix]
    public static void Postfix(LEV_SaveLoad __instance, FileInfo __state)
    {
        FileInfo levelWeJustSaved = __instance.GetLevelWeJustSaved();
        if (levelWeJustSaved != null)
        {
            if (levelWeJustSaved == __state)
                return;

            Postfixed?.Invoke(levelWeJustSaved.Name);
        }
    }
}