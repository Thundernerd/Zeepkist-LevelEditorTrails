using System.Collections.Generic;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails
{
    public class RecordedTrails
    {
        public struct RecordData
        {
            public readonly List<Vector3> Points;
            public Color Color;

            public RecordData(List<Vector3> points, Color color)
            {
                Points = points;
                Color = color;
            }
        }

        private static readonly List<RecordData> trails = new List<RecordData>();

        public static IReadOnlyList<RecordData> Trails => trails;

        public static void Add(List<Vector3> trail, Color color)
        {
            trails.Add(new RecordData(trail, color));

            if (trails.Count > Plugin.MaxRecordings.Value)
                trails.RemoveAt(0);
        }

        public static void Clear()
        {
            trails.Clear();
        }
    }
}
