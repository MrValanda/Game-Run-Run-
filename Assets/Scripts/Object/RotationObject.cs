using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float speed;
     private Rigidbody _rigidbody;

     private void Start()
     {
         _rigidbody = GetComponent<Rigidbody>();
     }

     void Update()
     {
         _rigidbody.angularVelocity = rotationVector.normalized * speed;
     }
}
