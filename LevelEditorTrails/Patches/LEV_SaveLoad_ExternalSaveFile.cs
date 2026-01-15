using System;
using System.IO;
using HarmonyLib;
using JetBrains.Annotations;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(LEV_SaveLoad), nameof(LEV_SaveLoad.ExternalSaveFile))]
internal class LEV_SaveLoad_ExternalSaveFile
{
    public static event Action<FileInfo> Postfixed;

    [UsedImplicitly]
    private static void Postfix(bool isTestMap, LEV_SaveLoad __instance)
    {
        if (!isTestMap)
        {
            Postfixed?.Invoke(__instance.GetLevelWeJustSaved());
        }
    }
}