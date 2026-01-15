using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails;

internal class CarInfo
{
    private readonly SetupCar _setupCar;
    private readonly New_ControlCar _controlCar;

    public CarInfo(SetupCar setupCar)
    {
        _setupCar = setupCar;
        _controlCar = _setupCar.cc;
    }
    
    public Vector3 Position => _controlCar.transform.position + _controlCar.transform.up; // Not sure if up is needed here
    public Vector3 Rotation => _controlCar.transform.eulerAngles;
    public byte State => _controlCar.currentZeepkistState;
    public bool IsParagliding => State == 3;

    public bool ArmsUp => IsParagliding ? _controlCar.GetPitchForward() : _controlCar.GetArmsUpHeld();
    public bool IsBraking => IsParagliding ? _controlCar.GetPitchBackward() : _controlCar.GetBrakeHeld();
    public float InputAxis => _controlCar.GetSteerActionButLimited();

    public float SteeringAngle =>
        (_setupCar.steeringModuleLeft.GetRealTurn() + _setupCar.steeringModuleRight.GetRealTurn()) * 0.5f /
        _setupCar.steeringModuleLeft.GetMaxSteerAngle();

    public float MaxSteerAngle => _setupCar.steeringModuleLeft.GetMaxSteerAngle();
    public float PitchAxis => _controlCar.GetAnalogPitchForInputDisplay();

    public float Velocity => _controlCar.GetLocalVelocity().magnitude * 3.6f;
}