using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    protected BoxCollider[] BoxColliders;

    public virtual void Use()
    {

    }

    public virtual void StopUse()
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
