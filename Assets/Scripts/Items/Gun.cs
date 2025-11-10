using System.Collections;
using System.Collections.Generic;
using CodingChallenge.Inventory;
using CodingChallenge.Player;
using UnityEngine;

namespace CodingChallenge.Items
{
    public class Gun : Item
    {
        [SerializeField] private GameObject _bulletGameObject;
        [SerializeField] private Transform _bulletExitPoint;
        [SerializeField] private GameObject _crosshair;

        private List<GameObject> _bulletPool = new();
        private int _magazineSize;
        private bool _isAutoFiring;

        /// <summary>
        /// Initiates the bullet object pool to pull the bullets that the gun has to fire from;
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            _magazineSize = 30;
        }

        public override void Equip(Hand hand)
        {
            _crosshair.SetActive(true);
            base.Equip(hand);
        }

        public override void DeEquip()
        {
            _crosshair.SetActive(false);
            base.DeEquip();
        }

        public override IEnumerator Using()
        {
            if (!_isSecondaryMode)
            {
                Fire();
                yield break;
            }
            _isAutoFiring = true;
            while (_isAutoFiring)
            {
                Fire();
                yield return new WaitForSeconds(0.25f);
            }
        }

        public override void StopUsing()
        {
            _isAutoFiring = false;
        }

        /// <summary>
        /// Reload the guns magazine size back to 30;
        /// </summary>
        public void Reload()
        {
            _magazineSize = 30;
        }

        private void Start()
        {
            for (int i = 1; i <= _magazineSize; i++)
            {
                GameObject bullet = Instantiate(_bulletGameObject);
                bullet.SetActive(false);
                _bulletPool.Add(bullet);
            }
        }

        /// <summary>
        /// Fires the rifle;
        /// </summary>
        private void Fire()
        {
            if (_magazineSize <= 0)
                return;
            GameObject currentBullet = _bulletPool[_magazineSize - 1];
            _magazineSize -= 1;
            currentBullet.SetActive(true);

            currentBullet.transform.position = _bulletExitPoint.position;
            currentBullet.transform.rotation = _bulletExitPoint.rotation;

            if (Physics.Raycast(transform.parent.position, transform.forward, out RaycastHit rayCastHit, Mathf.Infinity))
                currentBullet.GetComponent<Bullet>().Fire(rayCastHit.point);
            else
                currentBullet.GetComponent<Bullet>().Fire(transform.parent.forward * 1000);
        }
    }
}
