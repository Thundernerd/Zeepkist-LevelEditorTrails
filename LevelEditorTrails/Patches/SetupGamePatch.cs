using HarmonyLib;
using JetBrains.Annotations;

namespace TrailRendererLevelEditor.Patches
{
    [HarmonyPatch(typeof(SetupGame), "SpawnPlayers")]
    public class SetupGamePatch
    {
        [UsedImplicitly]
        internal static void Postfix()
        {
            Events.DispatchSpawnedPlayers();
        }
    }
}
