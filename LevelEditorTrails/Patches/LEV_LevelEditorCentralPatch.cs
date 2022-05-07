using HarmonyLib;
using JetBrains.Annotations;
using TrailRendererLevelEditor.Components;
using UnityEngine;

namespace TrailRendererLevelEditor.Patches
{
    [HarmonyPatch(typeof(LEV_LevelEditorCentral), "Awake")]
    public class LEV_LevelEditorCentralPatch
    {
        [UsedImplicitly]
        internal static void Postfix()
        {
            GameObject trailHolder = GetTrailHolder();

            foreach (RecordedTrails.RecordData recordData in RecordedTrails.Trails)
            {
                GameObject instance = new GameObject("Trail");
                instance.transform.SetParent(trailHolder.transform);
                Trail trail = instance.AddComponent<Trail>();
                trail.Initialize(recordData.Points, recordData.Color);
            }
        }

        private static GameObject GetTrailHolder()
        {
            GameObject gameObject = GameObject.Find("TNRD.TrailHolder");
            if (gameObject != null) Object.Destroy(gameObject);

            return new GameObject("TNRD.TrailHolder");
        }
    }
}
