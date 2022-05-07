using HarmonyLib;
using JetBrains.Annotations;
using TNRD.Zeepkist.LevelEditorTrails.Components;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches
{
    [HarmonyPatch(typeof(LEV_TestMap), nameof(LEV_TestMap.TestMap))]
    public class LEV_TestMapPatch
    {
        [UsedImplicitly]
        internal static void Postfix()
        {
            Events.SpawnedPlayers -= OnSpawnedPlayers;
            Events.SpawnedPlayers += OnSpawnedPlayers;
        }

        private static void OnSpawnedPlayers()
        {
            Events.SpawnedPlayers -= OnSpawnedPlayers;

            GameObject soapbox = GameObject.Find("Soapbox(Clone)");
            soapbox.AddComponent<TrailRecorder>();
        }
    }
}
