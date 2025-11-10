using CodingChallenge.Armor;
using CodingChallenge.Interactable;
using CodingChallenge.Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodingChallenge.Inventory
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

        /// <summary>
        /// Gives the hand it's new items after a switch has occured;
        /// </summary>
        /// <param name="newItem">The new items that is held by that hand;</param>
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

        /// <summary>
        /// Drops the current item from the hand;
        /// </summary>
        /// <param name="selfdrop">Wether the item drops itself;</param>
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
            
            _interectableMask = LayerMask.GetMask("Item", "Armor", "Interactable");
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

        /// <summary>
        /// Determines what has to happen with the item that the raycast collides with;
        /// </summary>
        private void TryPickUp()
        {
            if (Physics.SphereCastNonAlloc(transform.position, .15f, transform.forward, _itemCastHits, 1f, _interectableMask) == 0)
                return;

            if (_itemCastHits[0].collider.GetComponent<InteractableObject>() != null)
            {
                _itemCastHits[0].collider.GetComponent<InteractableObject>().Use();
                return;
            }

            if (_armorSystem.Equip(_itemCastHits[0].collider.GetComponent<ArmorItem>()))
                return;

            if (_heldItem != null)
                return;

            if (!_inventorySystem.Equip(this, _itemCastHits[0].collider.GetComponent<Item>()))
                return;

            _canUse = true;
            _heldItem = _itemCastHits[0].collider.GetComponent<Item>();
        }

        /// <summary>
        /// Uses the item that is currently being held;
        /// </summary>
        private void Use()
        {
            if (_heldItem == null)
                return;

            StartCoroutine(_heldItem.Using());
        }

        /// <summary>
        /// Stops using the item that is currently being held;
        /// </summary>
        private void StopUse()
        {
            if (_heldItem == null)
                return;

            _heldItem.StopUsing();
        }

        /// <summary>
        /// Switches the use mode of the item that is currently being held;
        /// </summary>
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
