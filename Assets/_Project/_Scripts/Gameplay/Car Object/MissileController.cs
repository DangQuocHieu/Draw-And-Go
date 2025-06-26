using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isBoosting;
    private float _speedChange;
    private bool _canRotate = false;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private float _rotateTime;
    [SerializeField] private float _rotateDuration = 0.3f;
    [SerializeField] private float _rotationAngle = 45f;

    [SerializeField] private BoxCollider2D _headCarCollider;
    [SerializeField] private LayerMask _platformLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void StartBoosting(float speedChange)
    {
        _speedChange = speedChange;
        _isBoosting = true;
        GetComponent<CarMovement>().TurnOnEngine();
        GetComponent<CarMovement>().ChangeCarSpeed(speedChange);
    }

    private void HandleBoosting()
    {
        if (!_isBoosting) return;

    }

    private void HandleRotating()
    {
        if (transform.eulerAngles.z < 20f) return;
        if (_headCarCollider.IsTouchingLayers(_platformLayer) && !_canRotate)
        {
            _canRotate = true;
            _startRotation = transform.rotation;
            _targetRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + _rotationAngle);
            _rotateTime = 0f;
        }
        else if (_canRotate)
        {
            _rotateTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(_startRotation, _targetRotation, _rotateTime / _rotateDuration);
            if (_rotateTime >= _rotateDuration)
            {
                _canRotate = false;
            }
        }
    }
    void Update()
    {
        HandleRotating();
    }
    void FixedUpdate()
    {
        HandleBoosting();
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Wall") && !_canRotate)
    //     {
    //         _canRotate = true;
    //         _startRotation = transform.rotation;
    //         _targetRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + 60f);
    //         _rotateTime = 0f;
    //     }
    // }
}
