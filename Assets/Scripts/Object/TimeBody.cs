using System.Collections.Generic;
using UnityEngine;

struct TimePosition
{
    public Vector3 Position;
    public Quaternion Quaternion;
    public Vector3 Speed;
    public override bool Equals(object obj)
    {
        TimePosition timePosition = (TimePosition) obj;
        return Position==timePosition.Position
            && Quaternion==timePosition.Quaternion
            && Speed==timePosition.Speed;
    }
}
public class TimeBody : MonoBehaviour
{
    private PlayerController _playerController;
    private Rigidbody _rb;
    private Stack<TimePosition> _stack;
    private float _speed;
    private Vector3 _normolize;
    private Quaternion _quaternion;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
        _playerController = FindObjectOfType<PlayerController>();
        Debug.Log(_playerController);
        _rb = GetComponent<Rigidbody>();
        _stack=new Stack<TimePosition>();
        _playerController.activatingAbilityStopTime.AddListener(() =>
        {
            _normolize = _rb.velocity.normalized;
            _speed = _rb.velocity.magnitude;
            _quaternion = _transform.rotation;
          //  _rb.freezeRotation = true;
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
        });
        _playerController.deactivatingAbilityStopTime.AddListener(() =>
        {
            _rb.velocity = _normolize * _speed;
            _rb.isKinematic = false;
            
        });
        _playerController.activatingAbilityBackTime.AddListener(BackTime);
        _playerController.deactivatingAbilityBackTime.AddListener(() =>
        {
            TimePosition timePosition = new TimePosition
                {Position = transform.position, Quaternion = transform.rotation, Speed = _rb.velocity};
                _stack.Push(timePosition);

        });
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(_stack.Count);
    }

    void BackTime()
    {
        if(_stack.Count==0)
            return;
        TimePosition timePosition = _stack.Pop();
        transform.position = timePosition.Position;
        transform.rotation = timePosition.Quaternion;
        _rb.velocity = timePosition.Speed;
    }
}
