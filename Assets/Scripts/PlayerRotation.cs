using Unity.Cinemachine;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _camera;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }



    private void FixedUpdate()
    {
        Rotation();
    }

    private void Rotation()
    {
        float rotationAxis = _camera.PanAxis.Value;
        _rigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, rotationAxis, 0)));
    }
}
