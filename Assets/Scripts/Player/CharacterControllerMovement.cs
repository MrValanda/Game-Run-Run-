using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour,IMovement
{
    [SerializeField]private float _speed, _jumpHeight,_gravityScale,_cooldownJump;
    private CharacterController _characterController;
    private float _directionY;
    public bool UseGravity { get; set; }
    private bool _canJump = true;

    void Start()
    {
    UseGravity = true;
    _characterController = GetComponent<CharacterController>() ?? gameObject.AddComponent<CharacterController>();
    }

    public void AddJumps(int count)
    {
        
    }

    public void Movement(Vector3 direction,float speed=0)
    {
        if (speed <= 0)
            speed = _speed;
        direction = direction.normalized * speed;
        if (UseGravity)
        {
            _directionY -= _gravityScale * Time.deltaTime;
        }
        direction.y = _directionY;
        _characterController.Move(direction*Time.deltaTime);
    }
    public void Move(Vector3 direction)
    {
        _characterController.Move(direction);
    }

    public void Jump()
    {
        if (_canJump)
        {
            _directionY = _jumpHeight;
            _canJump = false;
            Invoke(nameof(ReadyJump), _cooldownJump);
        }
    }

    private void ReadyJump()
    {
        _canJump = true;
    }
    public bool OnGround() => _characterController.isGrounded;

}
