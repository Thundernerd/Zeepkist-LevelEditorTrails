using System.Linq;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class TrailRecorder : MonoBehaviour
{
    private Trail _trail;
    private CarInfo _carInfo;
    private float _time;
    
    private void Awake()
    {
        _trail = new Trail();
        _carInfo = new CarInfo(PlayerManager.Instance.currentMaster.carSetups.First());
    }

    private void OnDestroy()
    {
        TrailManager.AddTrail(_trail);
    }

    private void Update()
    {
        _trail.Frames.Add(new TrailFrame(_time, _carInfo));
        _time +=  Time.deltaTime;
    }
}