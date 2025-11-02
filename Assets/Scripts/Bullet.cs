using UnityEngine;

public class Bullet : Item
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BoxColliders = GetComponentsInChildren<BoxCollider>();
    }

    public void Fire()
    {
        Debug.Log("Fire");
    }
}
