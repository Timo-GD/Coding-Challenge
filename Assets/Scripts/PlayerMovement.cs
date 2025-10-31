using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpforce;
    private bool _isGrounded;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = Vector3.zero;
        HorizontalMovement(targetVelocity);
        VerticalMovement(targetVelocity);

    }

    private void HorizontalMovement(Vector3 horizontalVelocity)
    {
        horizontalVelocity += new Vector3(Vector3.forward.x * _speed, 0, Vector3.forward.z * _speed);
    }
    
    private void VerticalMovement(Vector3 verticalVelocity)
    {
        if (!_isGrounded)
            return;
        _rigidbody.AddForce(Vector3.up * _jumpforce, ForceMode.Impulse);
        _isGrounded = false;
    }
}
