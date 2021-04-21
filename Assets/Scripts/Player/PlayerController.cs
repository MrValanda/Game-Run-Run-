using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivatingAbilityStopTime?.Invoke();
        }

        if (!Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.E))
                UseAbilityBackTime?.Invoke();
            else AbilityNoUse?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            DeactivatingAbilityStopTime?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DeactivatingAbilityBackTime?.Invoke();
        }
    }
}
