using System.Collections;
using CodingChallenge.Inventory;
using UnityEngine;

namespace CodingChallenge.Items
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

        /// <summary>
        /// The general Using function that all items inherit from;
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator Using()
        {
            yield break;
        }

        /// <summary>
        /// The general StopUsing function that all items inherit from;
        /// </summary>
        public virtual void StopUsing()
        {

        }

        /// <summary>
        /// The general ModeSwitch function that all items inherit from;
        /// </summary>
        public virtual void ModeSwitch()
        {
            _isSecondaryMode = _isSecondaryMode ? false : true;
        }

        /// <summary>
        /// The general Equip function that all items inherit from;
        /// </summary>
        /// <param name="hand">The hand that the item is being equiped to;</param>
        public virtual void Equip(Hand hand)
        {
            _hand = hand;

            _rigidbody.useGravity = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            foreach (Collider collider in _colliders)
                collider.enabled = false;
        }

        /// <summary>
        /// The general DeEquip function that all items inherit from;
        /// </summary>
        public virtual void DeEquip()
        {
            _hand = null;

            _rigidbody.useGravity = true;

            foreach (Collider collider in _colliders)
                collider.enabled = true;
        }
    }
}
