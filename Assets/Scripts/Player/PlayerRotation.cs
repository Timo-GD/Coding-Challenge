using Unity.Cinemachine;
using UnityEngine;

namespace CodingChallenge.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private CinemachinePanTilt _camera;

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            Rotation();
        }

        private void Rotation()
        {
            float pannAxis = _camera.PanAxis.Value;
            float tiltAxis = _camera.TiltAxis.Value;

            transform.rotation = Quaternion.Euler(new Vector3(tiltAxis, pannAxis, 0));

            _rigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, pannAxis, 0)));
        }
    }
}
