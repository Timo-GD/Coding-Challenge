using System.Collections;
using Inventory;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        protected Rigidbody _rigidbody;
        protected Collider[] _colliders;
        protected Hand _hand;
        protected bool _isSecondaryMode;

        public virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _colliders = GetComponentsInChildren<Collider>();
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
            _isSecondaryMode = _isSecondaryMode ? false : true;
        }

        public virtual void Equip(Hand hand)
        {
            _hand = hand;

            _rigidbody.useGravity = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            foreach (Collider collider in _colliders)
                collider.enabled = false;
        }

        public virtual void DeEquip()
        {
            _hand = null;

            _rigidbody.useGravity = true;

            foreach (Collider collider in _colliders)
                collider.enabled = true;
        }
    }
}
