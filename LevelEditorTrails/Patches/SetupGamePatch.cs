using HarmonyLib;
using JetBrains.Annotations;

namespace TNRD.Zeepkist.LevelEditorTrails.Patches
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
