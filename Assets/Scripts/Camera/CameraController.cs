
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _sensitivity;
    
    private Transform _transform;
    
    private float _moveX, _moveY;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _moveX = Input.GetAxisRaw("Mouse X")*_sensitivity;
        _moveY -= Input.GetAxisRaw("Mouse Y")*_sensitivity*Time.deltaTime;

        _moveY = Mathf.Clamp(_moveY, -90, 90);
        
        _transform.localRotation = Quaternion.Euler(_moveY,0,0);
        _playerTransform.Rotate(Vector3.up * (_moveX * Time.fixedDeltaTime));
    }
}
