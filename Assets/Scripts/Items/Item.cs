using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    protected Collider[] Colliders;
    protected GameObject Hand;

    public virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
    }
    public virtual IEnumerator Using()
    {
        yield break;
    }

    public virtual void StopUsing()
    {
        
    }
    
    public virtual void ModeSwitch()
    {
        
    }

    public virtual void Equip(GameObject hand)
    {
        Hand = hand;
        Rigidbody.useGravity = false;
        Rigidbody.linearVelocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        foreach(Collider collider in Colliders)
            collider.enabled = false;
    }

    public virtual void DeEquip()
    {
        Hand = null;
        Rigidbody.useGravity = true;
        foreach(Collider collider in Colliders)
            collider.enabled = true;
    }
}
