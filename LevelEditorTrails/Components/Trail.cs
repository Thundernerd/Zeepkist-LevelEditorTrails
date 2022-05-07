using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TNRD.Zeepkist.LevelEditorTrails.Components
{
    public class Trail : MonoBehaviour
    {
        private LineRenderer line;

        public void Initialize(List<Vector3> points, Color color)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));

            line.startColor = color;
            line.endColor = color;

            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());

            line.widthMultiplier = Mathf.Clamp(Plugin.LineWidth.Value, Constants.MIN_WIDTH, Constants.MAX_WIDTH);

            line.numCapVertices = 5;
            line.numCornerVertices = 5;

            line.receiveShadows = false;
            line.shadowCastingMode = ShadowCastingMode.Off;

            line.enabled = Plugin.ShouldBeVisible;
        }

        private void Update()
        {
            if (Input.GetKeyDown(Plugin.KeyToggleVisibility.Value))
            {
                line.enabled = !line.enabled;
            }
            else if (Input.GetKeyDown(Plugin.KeyRemoveLines.Value))
            {
                Destroy(gameObject);
            }
        }
    }
}
