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
                Vector3 targetPosition = new Vector3(transform.position.x * 0.906f, transform.position.y, transform.position.z * 1.375f);
                StartCoroutine(OpenDoor(targetRotation, targetPosition));
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
                Vector3 targetPosition = new Vector3(transform.position.x * 1.103f, transform.position.y, transform.position.z * 0.727f);
                StartCoroutine(CloseDoor(targetRotation, targetPosition));
            }

            return base.Use();
        }

        private IEnumerator OpenDoor(Quaternion targetRotation, Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _openSpeed);
                yield return null;
            }
            transform.position = targetPosition;
            transform.rotation = targetRotation;
            _isClosed = true;
            _isUsed = false;
            yield return null;
        }
        
        private IEnumerator CloseDoor(Quaternion targetRotation, Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _openSpeed);
                yield return null;
            }
            transform.position = targetPosition;
            transform.rotation = targetRotation;
            _isClosed = false;
            _isUsed = false;
            yield return null;
        }
    }
}
