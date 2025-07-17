using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour, IMessageHandle, IStartable
{
    [SerializeField] public float _speed = 1500f;
    [SerializeField] private WheelJoint2D _backWheel;
    [SerializeField] private CircleCollider2D _backWheelCollider;
    [SerializeField] private WheelJoint2D _frontWheel;
    [SerializeField] private CircleCollider2D _frontWheelCollider;
    [SerializeField] private bool _useEngine = false;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _roadLayer;

    private Collider2D[] _carCollider;
    private Rigidbody2D _carRb;
    private bool _canMove = false;
    private bool _isGrounded;



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
        MessageManager.AddSubscriber(GameMessageType.OnLevelSetUp, this);
        MessageManager.AddSubscriber(GameMessageType.OnCustomLevelSetUp, this);

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == GameConstant.HOME_SCENE)
        {
            OnStart();
            StartCoroutine(LandingCoroutine());
        }
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnCarStarted, this);
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelSetUp, this);
        MessageManager.RemoveSubscriber(GameMessageType.OnCustomLevelSetUp, this);

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
        _isGrounded = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _roadLayer);
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
            case GameMessageType.OnLevelSetUp:
                LevelStat stat = (LevelStat)message.data[0];
                if (stat.UseEngine) StartCoroutine(LandingCoroutine());
                break;
            case GameMessageType.OnCarStarted:
                OnStart();
                break;
            case GameMessageType.OnCustomLevelSetUp:
                var data = (CustomLevelData)message.data[0];
                if (data.UseEngine) StartCoroutine(LandingCoroutine());
                break;
        }
    }

    private IEnumerator LandingCoroutine()
    {
        while (true)
        {
            if (_isGrounded) break;
            yield return null;
        }
        TurnOnEngine();
        _carRb.constraints = RigidbodyConstraints2D.None;
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
        StartCoroutine(OnGravityFlipCoroutine());
    }

    private IEnumerator OnGravityFlipCoroutine()
    {
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        _carRb.centerOfMass = new Vector2(0, 0.5f);
        _carRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        _speed = -_speed;
        yield return new WaitUntil(() => !_isGrounded);
        StartCoroutine(LandingCoroutine());
    }

    public void TurnOnEngine()
    {
        Debug.Log("TURN ON ENGINE");
        _useEngine = true;
    }

    public void TurnOffEngine()
    {
        _useEngine = false;
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
