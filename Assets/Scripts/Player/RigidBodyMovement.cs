using UnityEngine;

public class RigidBodyMovement : MonoBehaviour,IMovement
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _cooldownJump;
    [SerializeField] private int _countJump;
    [SerializeField] private LayerMask _layerMask;
    
    private Rigidbody _rigidbody;
    private Transform _transform;
    private int _currentCountJump;
    public bool UseGravity
    {
        get => _rigidbody.useGravity;
        set => _rigidbody.useGravity = value;
    }

    private bool _canJump=true;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) _rigidbody = gameObject.AddComponent<Rigidbody>();

        _rigidbody.freezeRotation = true;
    }

    public void Movement(Vector3 direction,float speed = 0)
    {
        if (OnGround())
            _currentCountJump = 0;
        
        if (speed <= 0)
            speed = _speed;
        direction = direction.normalized * speed;
        direction.y=_rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction;
    }

    public void Jump()
    {
        if (_canJump && _currentCountJump <=_countJump)
        {
            Vector3 dir = _rigidbody.velocity;
            dir.y = _jumpForce;
            _rigidbody.velocity = dir;
            _canJump = false;
            _currentCountJump++;
            Invoke(nameof(ReadyJump), _cooldownJump);
        }
    }

    private void ReadyJump()
    {
        _canJump = true;
    }

    public bool OnGround()
    {
        return Physics.Raycast(_transform.position, -_transform.up.normalized, _transform.lossyScale.y / 2f + 0.6f,
            _layerMask);
    }

    public void AddJumps(int count)
    {
        if (_currentCountJump - count >= 0)
            _currentCountJump -= count;
    }
}