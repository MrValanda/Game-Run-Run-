using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour,IMovement
{
    [SerializeField]private float _speed, _jumpHeight,_gravityScale;
    private CharacterController _characterController;
    private float _directionY;
    public bool UseGravity { get; set; }

    void Start()
    {
    UseGravity = true;
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            _characterController = gameObject.AddComponent<CharacterController>();
        }
    }
    
    public void Movement(Vector3 direction)
    {
        direction = direction.normalized * _speed;
        if (UseGravity)
        {
            _directionY -= _gravityScale * Time.deltaTime;
            direction.y = _directionY;
        }

        _characterController.Move(direction*Time.deltaTime);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
            _directionY = _jumpHeight;
    }
    
}
