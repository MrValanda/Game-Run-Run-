using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private IMovement _playerMovement;
    private Transform _transform;
    private float _horizontalInputs, _verticalInputs;
    public UnityEvent activatingAbilityStopTime;
    public UnityEvent deactivatingAbilityStopTime;
    public UnityEvent activatingAbilityBackTime;
    public UnityEvent deactivatingAbilityBackTime;



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
            activatingAbilityStopTime.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            deactivatingAbilityStopTime.Invoke();
        }

        if (Input.GetKey(KeyCode.E))
        {
            activatingAbilityBackTime.Invoke();
        }
        else if (!Input.GetKey(KeyCode.Q))
        {
            deactivatingAbilityBackTime.Invoke();
        }
    }
}
