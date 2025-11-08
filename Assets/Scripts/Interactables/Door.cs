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

            float doorRotation = Mathf.Round(transform.localRotation.eulerAngles.y) / Mathf.Round(Mathf.Abs(transform.localRotation.eulerAngles.y));
            if (float.IsNaN(doorRotation))
                doorRotation = 1;

            _isUsed = true;
            if (!_isClosed)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - 90, transform.localRotation.eulerAngles.z);
                Vector3 targetPosition = transform.position + (transform.forward * doorRotation * .75f) + (transform.right * -doorRotation * .75f);
                StartCoroutine(OpenDoor(targetRotation, targetPosition));
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 90, transform.localRotation.eulerAngles.z);
                Vector3 targetPosition = transform.position + (transform.forward * -doorRotation * .75f) + (transform.right * -doorRotation * .75f);
                StartCoroutine(CloseDoor(targetRotation, targetPosition));
            }

            return base.Use();
        }

        private IEnumerator OpenDoor(Quaternion targetRotation, Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.5f)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _openSpeed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _openSpeed);
                yield return null;
            }
            transform.position = targetPosition;
            transform.localRotation = targetRotation;
            _isClosed = true;
            _isUsed = false;
            yield return null;
        }

        private IEnumerator CloseDoor(Quaternion targetRotation, Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.5f)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _openSpeed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _openSpeed);
                yield return null;
            }
            transform.position = targetPosition;
            transform.localRotation = targetRotation;
            _isClosed = false;
            _isUsed = false;
            yield return null;
        }
    }
}
