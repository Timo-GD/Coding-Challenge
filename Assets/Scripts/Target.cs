using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    [SerializeField] InputAction _interact;
    private LayerMask _itemLayer;
    private LayerMask _armorLayer;
    private void Awake()
    {
        _itemLayer = LayerMask.GetMask("Item");
        _armorLayer = LayerMask.GetMask("Armor");
        _interact.performed += context => CheckInteraction();
    }
    private void CheckInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit armorHit, Mathf.Infinity, _armorLayer) && GetComponentInParent<ArmorSystem>().Equip(armorHit.collider.gameObject.tag))
                armorHit.collider.GetComponent<Item>().Equip();
        else if(Physics.Raycast(transform.position, transform.forward, out RaycastHit itemHit, Mathf.Infinity, _itemLayer) && GetComponentInParent<InverntorySystem>().Equip(itemHit.collider.gameObject))
                itemHit.collider.GetComponent<Item>().Equip();
    }

    private void OnDestroy()
    {
        _interact.performed -= context => CheckInteraction();
    }

    private void OnEnable()
    {
        _interact.Enable();
    }

    private void OnDisable()
    {
        _interact.Disable();
    }
}
