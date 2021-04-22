using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private KeyCode _backTimeKey, _timeStopKey;
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
        Inputs();
    }

    private void FixedUpdate()
    {
       _playerMovement.Movement(_transform.forward * _verticalInputs + _transform.right * _horizontalInputs);
       if (Input.GetKey(KeyCode.Space))
       {
           _playerMovement.Jump();
       }

    }
    
    private void Inputs()
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
