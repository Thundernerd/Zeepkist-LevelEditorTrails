using System;
using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches;

[HarmonyPatch(typeof(LEV_SaveLoad),nameof(LEV_SaveLoad.ExternalLoad))]
internal class LEV_SaveLoad_ExternalLoad
{
    public static event Action<string> Postfixed;
    
    [HarmonyPostfix]
    public static void Postfix(string filePath)
    {
        Postfixed?.Invoke(filePath);
    }
}