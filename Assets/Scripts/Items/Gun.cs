using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Gun : Item
    {
        [SerializeField] private GameObject _bulletGameObject;
        [SerializeField] private Transform _bulletExitPoint;

        private List<GameObject> _bulletPool = new();
        private int _magazineSize;
        private bool _isAutoFiring;

        public override void Awake()
        {
            base.Awake();
            _magazineSize = 30;
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
