    using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour, IMessageHandle, IStartable
{
    [SerializeField] public float _speed = 1500f;
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
        _carRb.centerOfMass = new Vector2(0, -0.5f);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _speed = -_speed;
        }
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
        _speed = -_speed;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    public void TurnOnEngine()
    {
        _useEngine = true;
    }

    public void ChangeCarSpeed(float speed)
    {
        _speed = speed;
    }

    public void OnMissileBoost(PhysicsMaterial2D boostMaterial, float speed)
    {
        _backWheelCollider.sharedMaterial = boostMaterial;
        _frontWheelCollider.sharedMaterial = boostMaterial;
        TurnOnEngine();
        ChangeCarSpeed(speed);
    }

}
