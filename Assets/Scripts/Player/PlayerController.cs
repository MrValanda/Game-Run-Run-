using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    private CharacterController _characterController;
    private Transform _transform;
    private float _horizontalInputs, _verticalInputs, _directionY;
    private Vector3 _velocity;
    public UnityEvent activatingAbilityStopTime;
    public UnityEvent deactivatingAbilityStopTime;
    public UnityEvent activatingAbilityBackTime;
    public UnityEvent deactivatingAbilityBackTime;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _velocity = (_transform.forward * _verticalInputs + _transform.right * _horizontalInputs).normalized *
                    moveSpeed;
        if (_characterController.isGrounded)
        {
            _directionY = -1;
            if (Input.GetKey(KeyCode.Space))
            {
                _directionY = jumpHeight;
            }
        }

        CalculateGravity();
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void CalculateGravity()
    {
        _directionY -= gravityScale * Time.deltaTime;
        _velocity.y = _directionY;
    }

    private void Inputs()
    {
        _horizontalInputs = Input.GetAxisRaw("Horizontal");
        _verticalInputs = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activatingAbilityStopTime.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            deactivatingAbilityStopTime.Invoke();
        }

        if (Input.GetKey(KeyCode.E))
        {
            activatingAbilityBackTime.Invoke();
        }else if (!Input.GetKey(KeyCode.Q))
        {
            deactivatingAbilityBackTime.Invoke();
        }
        
        
    }
}
