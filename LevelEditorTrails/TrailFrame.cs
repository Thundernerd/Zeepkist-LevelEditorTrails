using Newtonsoft.Json;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal readonly struct TrailFrame
{
    [JsonProperty("t")] public readonly float Time;
    [JsonProperty("p")] public readonly Vector3 Position;
#if false
    [JsonProperty("r")] public readonly Vector3 Rotation;
    [JsonProperty("s")] public readonly byte State;
    [JsonProperty("v")] public readonly float Velocity;
    [JsonProperty("a")] public readonly bool ArmsUp;
    [JsonProperty("b")] public readonly bool IsBraking;
    [JsonProperty("i")] public readonly float InputAxis;
    [JsonProperty("l")] public readonly float SteeringAngle;
    [JsonProperty("m")] public readonly float MaxSteeringAngle;
    [JsonProperty("n")] public readonly float PitchAxis;
#endif

    public TrailFrame(float time, CarInfo carInfo)
    {
        Time = time;
        Position = carInfo.Position;

#if false
        Rotation = carInfo.Rotation;
        State = carInfo.State;
        Velocity = carInfo.Velocity;
        ArmsUp = carInfo.ArmsUp;
        IsBraking = carInfo.IsBraking;
        InputAxis = carInfo.InputAxis;
        SteeringAngle = carInfo.SteeringAngle;
        MaxSteeringAngle = carInfo.MaxSteerAngle;
        PitchAxis = carInfo.PitchAxis;
#endif
    }
}