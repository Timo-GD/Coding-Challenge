using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    
    private void Update()
    {
        transform.LookAt(new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z));
    }
}
