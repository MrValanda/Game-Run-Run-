using UnityEngine;

public class RigidBodyMovement : MonoBehaviour,IMovement
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _layerMask;
    private Rigidbody _rigidbody;
    private Transform _transform;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();

        _rigidbody.freezeRotation = true;
    }

    public void Movement(Vector3 direction)
    {
        direction = direction.normalized * _speed;
        direction.y=_rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

    public void Jump()
    {
        if (onGround())
        {
            Vector3 dir = _rigidbody.velocity;
            dir.y = _jumpForce;
            _rigidbody.velocity = dir;
        }
    }

    private bool onGround()
    {
        return Physics.Raycast(_transform.position, -_transform.up.normalized, _transform.lossyScale.y / 2f + 0.6f,
            _layerMask);
    }
}
