using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody Rigidbody;
    protected BoxCollider BoxCollider;
    
    public virtual void Use()
    {

    }

    public virtual void Equip()
    {
        Rigidbody.useGravity = false;
        BoxCollider.enabled = false;
    }

    public virtual void DeEquip()
    {
        Rigidbody.useGravity = true;
        BoxCollider.enabled = true;
    }
}
