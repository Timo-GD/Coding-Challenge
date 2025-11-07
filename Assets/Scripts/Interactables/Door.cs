using System.Collections;
using UnityEngine;

namespace CodingChallenge.Interactable
{
    public class Door : InteractableObject
    {
        private bool _isClosed;
        private bool _isUsed;
        private float _openSpeed = 5f;
        public override bool Use()
        {
            if (_isUsed)
                return false;

            _isUsed = true;

            if (!_isClosed)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
                StartCoroutine(OpenDoor(targetRotation));
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
                StartCoroutine(CloseDoor(targetRotation));
            }

            return base.Use();
        }

        private IEnumerator OpenDoor(Quaternion targetRotation)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.0005f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
                yield return null;
            }
            _isClosed = true;
            _isUsed = false;
            yield return null;
        }
        
        private IEnumerator CloseDoor(Quaternion targetRotation)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.0005f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
                yield return null;
            }
            _isClosed = false;
            _isUsed = false;
            yield return null;
        }
    }
}
