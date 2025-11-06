using Armors;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class Hand : MonoBehaviour
    {
        public Item HeldItem => _heldItem;

        [SerializeField] private InputAction _pickup;
        [SerializeField] private InputAction _drop;
        [SerializeField] private InputAction _switchUseMode;

        private RaycastHit[] _itemCastHits = new RaycastHit[1];
        private Item _heldItem;
        private InventorySystem _inventorySystem;
        private ArmorSystem _armorSystem;
        private LayerMask _interectableMask;
        private bool _canUse;

        public void SwitchItem(Item newItem)
        {
            _heldItem = newItem;
            if (_heldItem != null)
            {
                _heldItem.Equip(this);
                _canUse = true;
            }
            else
            {
                _canUse = false;
            }
        }

        public void DropItem(bool selfdrop)
        {
            if (_heldItem == null)
                return;

            if (!_inventorySystem.DeEquip(this, selfdrop))
                return;

            _canUse = false;
            _heldItem = null;
        }

        private void Awake()
        {

            _pickup.performed += context => CheckUse();
            _pickup.canceled += context => StopUse();
            _drop.performed += context => DropItem(false);
            _switchUseMode.performed += context => SwitchUseMode();
            
            _interectableMask = LayerMask.GetMask("Item", "Armor");
            _inventorySystem = GetComponentInParent<InventorySystem>();
            _armorSystem = GetComponentInParent<ArmorSystem>();
        }

        private void CheckUse()
        {
            if (!_canUse)
                TryPickUp();
            else
                Use();
        }

        private void TryPickUp()
        {
            if (Physics.SphereCastNonAlloc(transform.position, .6f, transform.forward, _itemCastHits, 0, _interectableMask) == 0)
                return;

            if (_armorSystem.Equip(_itemCastHits[0].collider.GetComponent<Armor>()))
                return;

            if (_heldItem != null)
                return;


            if (!_inventorySystem.Equip(this, _itemCastHits[0].collider.GetComponent<Item>()))
                return;

            _canUse = true;
            _heldItem = _itemCastHits[0].collider.GetComponent<Item>();
        }

        private void Use()
        {
            if (_heldItem == null)
                return;

            StartCoroutine(_heldItem.Using());
        }

        private void StopUse()
        {
            if (_heldItem == null)
                return;

            _heldItem.StopUsing();
        }

        private void SwitchUseMode()
        {
            if (_heldItem == null)
                return;

            _heldItem.ModeSwitch();
        }

        private void OnDestroy()
        {
            _pickup.performed -= context => CheckUse();
            _pickup.canceled -= context => StopUse();
            _drop.performed -= context => DropItem(false);
            _switchUseMode.performed -= context => SwitchUseMode();
        }

        private void OnEnable()
        {
            _pickup.Enable();
            _drop.Enable();
            _switchUseMode.Enable();
        }

        private void OnDisable()
        {
            _pickup.Disable();
            _drop.Disable();
            _switchUseMode.Disable();
        }
    }
}
