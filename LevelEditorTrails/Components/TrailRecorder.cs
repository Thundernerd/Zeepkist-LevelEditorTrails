using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails.Components
{
    public class TrailRecorder : MonoBehaviour
    {
        private bool isRunning;
        private Coroutine recordRoutine;

        private readonly List<Vector3> data = new List<Vector3>();

        private void Awake()
        {
            isRunning = true;
            recordRoutine = StartCoroutine(RecordEnumerator());
        }

        private void OnDestroy()
        {
            StopCoroutine(recordRoutine);
            RecordedTrails.Add(data, ColorManager.GetColor());
            isRunning = false;
        }

        private IEnumerator RecordEnumerator()
        {
            int lod = Mathf.Clamp(Plugin.LevelOfDetail.Value, Constants.MIN_LOD, Constants.MAX_LOD);
            float delay = 1f / lod;
            Transform t = transform;

            while (isRunning)
            {
                Vector3 p = t.position + t.up;
                data.Add(p);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
