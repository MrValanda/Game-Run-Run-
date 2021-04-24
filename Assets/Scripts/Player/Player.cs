using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private KeyCode _backTimeKey, _timeStopKey;
    
    [SerializeField] [Space(14)] [Range(0f, 15f)]
    private float _timeWallRun, _cooldownWallRun;

    [SerializeField] private LayerMask _layerMaskForWallRun;

    private IMovement _playerMovement;
    private Transform _transform;

    private float _currentTimeWallRun;
    
    private float _horizontalInputs, _verticalInputs;
    public event Action ActivatingAbilityStopTime;
    public event Action DeactivatingAbilityStopTime;
    public event Action UseAbilityBackTime;
    public event Action DeactivatingAbilityBackTime;
    public event Action AbilityNoUse;


    private bool _canWallRun=true,_onWallRun;
    
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _playerMovement = GetComponent<IMovement>();
    }


    private void Update()
    {
        inputs();
    }

    private void FixedUpdate()
    {
        wallRun();
        if (!_onWallRun)
            _playerMovement.Movement(_transform.forward * _verticalInputs + _transform.right * _horizontalInputs);
        if (Input.GetKey(KeyCode.Space))
        {
            _playerMovement.Jump();
        }
    }

    private void wallRun()
    {
        if (_canWallRun && !_playerMovement.OnGround())
        {
            if (Physics.Raycast(_transform.position, _transform.right.normalized,
                _transform.lossyScale.x / 2f + 0.6f, _layerMaskForWallRun))
            {
                moveWallRun();
            }
            else if (Physics.Raycast(_transform.position, -_transform.right.normalized,
                _transform.lossyScale.x / 2f + 0.6f, _layerMaskForWallRun))
            {
                moveWallRun();
            }
            else
            {
                _onWallRun = false;
                _currentTimeWallRun = Mathf.Lerp(_currentTimeWallRun, 0, Time.deltaTime);
            }
            Debug.Log(_currentTimeWallRun);

        }
    }

    private void moveWallRun()
    {
        _currentTimeWallRun += Time.deltaTime;
        if (_currentTimeWallRun >= _timeWallRun)
        {
            _canWallRun = false;
            _onWallRun = false;
            Invoke(nameof(readyWallRunning), _cooldownWallRun);
        }
        else
        {
            _onWallRun = true;
            _playerMovement.Movement(_transform.forward,11);
        }
    }

    private void readyWallRunning()
    {
        _canWallRun = true;
        _currentTimeWallRun = 0;
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
