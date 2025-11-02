using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    protected BoxCollider[] BoxColliders;

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
        foreach(BoxCollider collider in BoxColliders)
            collider.enabled = false;
    }

    public virtual void DeEquip()
    {
        Rigidbody.useGravity = true;
        foreach(BoxCollider collider in BoxColliders)
            collider.enabled = true;
    }
}
