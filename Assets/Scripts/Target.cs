using UnityEngine;

public class Target : MonoBehaviour
{
    private LayerMask _layerMask;
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Item");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, _layerMask))
            Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);
    }
}
