using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private KeyCode _backTimeKey, _timeStopKey;
    
    [SerializeField] private LayerMask _layerMaskForWallRun;
    [SerializeField] private float _wallrunForce;

private     bool _canWallRun;
private bool _isWallRunning,_isWallRight;

    private IMovement _playerMovement;
    private Transform _transform;

    
    private float _horizontalInputs, _verticalInputs;
    public event Action ActivatingAbilityStopTime;
    public event Action DeactivatingAbilityStopTime;
    public event Action UseAbilityBackTime;
    public event Action DeactivatingAbilityBackTime;
    public event Action AbilityNoUse;


    
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _playerMovement = GetComponent<IMovement>();
    }


    private void Update()
    {
        inputs();
        WallRunInput();
    }
    private void WallRunInput() 
    {
        if(Input.GetKey(Key.)&&_horizontalInputs != 0 && _canWallRun) StartWallrun();
    }
    private void StartWallrun()
    {
        _isWallRunning = true;
        Vector3 dir = _transform.forward * _wallrunForce;
        if (_isWallRight)
            dir += _transform.right * _wallrunForce / 5f;
        else 
            dir += -_transform.right * _wallrunForce / 5f;

        _playerMovement.Move(dir);
    }
    
    private void StopWallRun()
    {
        _isWallRunning = false;
       
    }
    private void CheckForWall() 
    {
        _isWallRight = Physics.Raycast(_transform.position, _transform.right, 1f, _layerMaskForWallRun);
        _canWallRun = _isWallRight || Physics.Raycast(_transform.position, -_transform.right, 1f, _layerMaskForWallRun);
        if (!_canWallRun) StopWallRun();
      //  if (_canWallRun) _playerMovement.AddJumps(1);
    }
    private void FixedUpdate()
    {
        CheckForWall();
        if (!_isWallRunning)
            _playerMovement.Movement(_transform.forward * _verticalInputs + _transform.right * _horizontalInputs);
        if (Input.GetKey(KeyCode.Space))
        {
            _playerMovement.Jump();
        }
    }

   
    
    private void inputs()
    {
        _horizontalInputs = Input.GetAxisRaw("Horizontal");
        _verticalInputs = Input.GetAxisRaw("Vertical");
        timeStopAbility();
        backTimeAbility();
    }

    private void backTimeAbility()
    {
        if (Input.GetKeyUp(_backTimeKey))
        {
            DeactivatingAbilityBackTime?.Invoke();
        }
        if (!Input.GetKey(_timeStopKey))
        {
            if (Input.GetKey(KeyCode.E))
                UseAbilityBackTime?.Invoke();
            else AbilityNoUse?.Invoke();
        }
    }

    private void timeStopAbility()
    {
        if (Input.GetKeyDown(_timeStopKey))
        {
            ActivatingAbilityStopTime?.Invoke();
        }
        if (Input.GetKeyUp(_timeStopKey))
        {
            DeactivatingAbilityStopTime?.Invoke();
        }
    }
}
