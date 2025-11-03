using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction _move;
    [SerializeField] private InputAction _jump;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    private bool _isGrounded;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jump.performed += context => VerticalMovement();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {

        Vector3 horizontalVelocity = Vector3.zero;

        horizontalVelocity = transform.forward * _move.ReadValue<Vector2>().y + transform.right * _move.ReadValue<Vector2>().x;

        horizontalVelocity.Normalize();
        horizontalVelocity *= _speed;

        _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z);
    }

    private void VerticalMovement()
    {
        if (!_isGrounded)
            return;
        _rigidbody.AddForce(Vector3.up * _jumpforce, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }

    private void OnDestroy()
    {
        _jump.performed -= context => VerticalMovement();
    }

    private void OnEnable()
    {
        _move.Enable();
        _jump.Enable();
    }

    private void ODisable()
    {
        _move.Disable();
        _jump.Disable();
    }
}
