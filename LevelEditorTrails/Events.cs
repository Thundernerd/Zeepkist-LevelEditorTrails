using System;

namespace TrailRendererLevelEditor
{
    public static class Events
    {
        public static event Action SpawnedPlayers;
        
        public static void DispatchSpawnedPlayers()
        {
            SpawnedPlayers?.Invoke();
        }
    }
}
