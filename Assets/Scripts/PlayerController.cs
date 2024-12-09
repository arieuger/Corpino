using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;

    private CharacterController _controller;
    private float _verticalVelocity;
    private float _gravity = 9.81f;
    private Transform _cameraTransform;
    private float _xRotation = 0f;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        _controller.Move(moveDirection * (speed * Time.deltaTime));

        // Aplicar gravedad
        if (!_controller.isGrounded)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
        }
        else if (_verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        Vector3 gravityEffect = new Vector3(0, _verticalVelocity, 0);
        _controller.Move(gravityEffect * Time.deltaTime);

        // Rotación da cámara en horizontal
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX); 
        
        // Rotación da cámara en vertical
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); 
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
