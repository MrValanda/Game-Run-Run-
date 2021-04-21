using System.Collections.Generic;
using UnityEngine;

struct TimePosition
{
    public Vector3 Position;
    public Quaternion Quaternion;
    public Vector3 Speed;
    public Vector3 AngularVelocity;
}
public class TimeBody : MonoBehaviour
{
    private PlayerController _playerController;
    private Rigidbody _rb;
    private Stack<TimePosition> _stack;
    private Vector3 _velocity;
    private Vector3 _angularVelocity;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
        _playerController = FindObjectOfType<PlayerController>();
        Debug.Log(_playerController);
        _rb = GetComponent<Rigidbody>();
        _stack=new Stack<TimePosition>();
        _playerController.ActivatingAbilityStopTime+= activatingStopTime;
        _playerController.DeactivatingAbilityStopTime+=  deactivatingStopTime;
        _playerController.UseAbilityBackTime += backTime;
        _playerController.AbilityNoUse+=saveTimePosition;
        _playerController.DeactivatingAbilityBackTime += stopBackTime;
    }

    private void activatingStopTime()
    {
        _velocity = _rb.velocity;
        _angularVelocity = _rb.angularVelocity;
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
    }

    private void deactivatingStopTime()
    {
        _rb.velocity = _velocity;
        _rb.angularVelocity = _angularVelocity;
        _rb.isKinematic = false;
    }

    private void saveTimePosition()
    {
        TimePosition timePosition = new TimePosition
        {
            Position = _transform.position, Quaternion = _transform.rotation, Speed = -_rb.velocity,
            AngularVelocity = -_rb.angularVelocity
        };
        _stack.Push(timePosition);
    }

    private void backTime()
    {
        if (_stack.Count == 0)
            return;
        
        TimePosition timePosition = _stack.Pop();
        //_transform.rotation= timePosition.Quaternion; 
        _rb.velocity = timePosition.Speed;
        _rb.angularVelocity = timePosition.AngularVelocity;
    }

    private void stopBackTime()
    {
        _rb.velocity = -_rb.velocity;
        _rb.angularVelocity = -_rb.angularVelocity;
    }
}
