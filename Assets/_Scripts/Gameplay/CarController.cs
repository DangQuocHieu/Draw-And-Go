using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float _speed = 1500f;
    [SerializeField] private WheelJoint2D _backWheel;
    [SerializeField] private WheelJoint2D _frontWheel;

    private float movement;
    void Update()
    {
        movement = -Input.GetAxisRaw("Horizontal") * _speed;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (movement == 0f)
        {
            _backWheel.useMotor = false;
            _frontWheel.useMotor = false;
        }
        else
        {
            _backWheel.useMotor = true;
            _frontWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = _backWheel.motor.maxMotorTorque };
            _backWheel.motor = motor;
            _frontWheel.motor = motor;
            
        }
    }
}
