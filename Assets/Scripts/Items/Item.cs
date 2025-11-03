using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    protected Collider[] Colliders;

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

    public virtual void Equip()
    {
        Rigidbody.useGravity = false;
        foreach(Collider collider in Colliders)
            collider.enabled = false;
    }

    public virtual void DeEquip()
    {
        Rigidbody.useGravity = true;
        foreach(Collider collider in Colliders)
            collider.enabled = true;
    }
}
