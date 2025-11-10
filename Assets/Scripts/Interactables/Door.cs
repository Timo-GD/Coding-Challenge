using System.Collections;
using UnityEngine;

namespace CodingChallenge.Interactable
{
    public class Door : InteractableObject
    {
        private bool _isOpen;
        private bool _isUsed;
        private float _openSpeed = 5f;

        public override bool Use()
        {
            if (_isUsed)
                return false;

            //Calculates wether the door is already at an angle and how to rotate the door based on this angle;
            float doorRotation = Mathf.Round(transform.rotation.eulerAngles.y) / Mathf.Round(Mathf.Abs(transform.rotation.eulerAngles.y));
            if (float.IsNaN(doorRotation))
                doorRotation = 1;

            _isUsed = true;
            if (_isOpen)
            {
                //The target position and rotations for closing the door;
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
                Vector3 targetPosition = transform.position + (transform.forward * -doorRotation * .75f) + (transform.right * -doorRotation * .75f);
                StartCoroutine(MoveDoor(targetRotation, targetPosition));
                _isOpen = false;
            }
            else
            {
                //The target position and rotations for opening the door;
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
                Vector3 targetPosition = transform.position + (transform.forward * doorRotation * .75f) + (transform.right * -doorRotation * .75f);
                StartCoroutine(MoveDoor(targetRotation, targetPosition));
                _isOpen = true;
            }

            return _isUsed;
        }

        /// <summary>
        /// Moves the door to the target position and rotation;
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveDoor(Quaternion targetRotation, Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles) > 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _openSpeed);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _openSpeed);
                yield return null;
            }

            transform.position = targetPosition;
            transform.rotation = targetRotation;

            _isUsed = false;
            yield return null;
        }
    }
}
