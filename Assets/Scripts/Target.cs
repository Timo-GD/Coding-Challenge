using UnityEngine;

public class Target : MonoBehaviour
{
    private LayerMask _layerMask;
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Ignore Raycast");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);
    }
}
