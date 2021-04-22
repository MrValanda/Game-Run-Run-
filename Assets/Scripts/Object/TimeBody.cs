using System.Collections.Generic;
using UnityEngine;

struct TimePosition
{
    public Vector3 Speed;
    public Vector3 AngularVelocity;
}
public class TimeBody : MonoBehaviour
{
    private Player _player;
    private Rigidbody _rb;
    private Stack<TimePosition> _stack;
    private Vector3 _velocity;
    private Vector3 _angularVelocity;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody>();
        _stack=new Stack<TimePosition>();
        _player.ActivatingAbilityStopTime+= activatingStopTime;
        _player.DeactivatingAbilityStopTime+=  deactivatingStopTime;
        _player.UseAbilityBackTime += backTime;
        _player.AbilityNoUse+=saveTimePosition;
        _player.DeactivatingAbilityBackTime += stopBackTime;
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
            Speed = -_rb.velocity,
            AngularVelocity = -_rb.angularVelocity
        };
        _stack.Push(timePosition);
    }

    private void backTime()
    {
        if (_stack.Count == 0)
            return;
        
        TimePosition timePosition = _stack.Pop();
        _rb.velocity = timePosition.Speed;
        _rb.angularVelocity = timePosition.AngularVelocity;
    }

    private void stopBackTime()
    {
        _rb.velocity = -_rb.velocity;
        _rb.angularVelocity = -_rb.angularVelocity;
    }
}
