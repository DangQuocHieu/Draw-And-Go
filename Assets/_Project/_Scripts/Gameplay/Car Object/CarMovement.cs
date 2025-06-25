using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour, IMessageHandle, IStartable
{
    [SerializeField] private float _speed = 1500f;
    [SerializeField] private WheelJoint2D _backWheel;
    [SerializeField] private CircleCollider2D _backWheelCollider;
    [SerializeField] private WheelJoint2D _frontWheel;
    [SerializeField] private CircleCollider2D _frontWheelCollider;
    [SerializeField] private bool _useEngine = true;

    private Collider2D[] _carCollider;


    private Rigidbody2D _carRb;

    private bool _canMove = false;

    void Awake()
    {
        _carRb = GetComponent<Rigidbody2D>();
        _carCollider = GetComponents<Collider2D>();
        OnIdle();
    }

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnCarStarted, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnCarStarted, this);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (!_useEngine) return;
        if (!_canMove) return;
        _backWheel.useMotor = true;
        _frontWheel.useMotor = true;
        JointMotor2D motor = new JointMotor2D { motorSpeed = -_speed, maxMotorTorque = _backWheel.motor.maxMotorTorque };
        _backWheel.motor = motor;
        _frontWheel.motor = motor;
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnCarStarted:
                OnStart();
                break;
        }
    }
    public void OnStart()
    {
        _carRb.bodyType = RigidbodyType2D.Dynamic;
        _canMove = true;
        _backWheelCollider.enabled = true;
        _frontWheelCollider.enabled = true;

        foreach (var colldier in _carCollider)
        {
            colldier.enabled = true;
        }
    }

    public void OnIdle()
    {
        _carRb.bodyType = RigidbodyType2D.Static;
        _backWheelCollider.enabled = false;
        _frontWheelCollider.enabled = false;

        foreach (var colldier in _carCollider)
        {
            colldier.enabled = false;
        }
    }

    public void OnGravityFlip()
    {
        if (!_useEngine) return;
        _speed = -_speed;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

}
