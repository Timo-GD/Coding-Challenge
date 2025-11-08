using System.Collections;
using UnityEngine;

namespace CodingChallenge.Interactable
{
    public class Button : InteractableObject
    {
        private Vector3 _oldPosition;
        private Vector3 _targetPosition;
        private Color _color;
        private float _moveSpeed = 5f;
        private bool _isPressed;

        public delegate void Pressed(Color buttonColor);
        public event Pressed OnPress;

        private void Awake()
        {
            _color = GetComponent<Renderer>().material.color;
        }
        public override bool Use()
        {
            if (_isPressed)
                return false;

            _oldPosition = transform.position;

            _targetPosition = transform.position;
            _targetPosition.y -= 0.15f;

            _isPressed = true;
            OnPress?.Invoke(_color);
            StartCoroutine(GoToPosition());
            return true;
        }

        private IEnumerator GoToPosition()
        {
            while (Vector3.Distance(transform.position, _targetPosition) > 0.005f)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
                yield return null;
            }

            transform.position = _targetPosition;
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(ReturnToPosition());
        }

        private IEnumerator ReturnToPosition()
        {
            while (Vector3.Distance(transform.position, _oldPosition) > 0.005f)
            {
                transform.position = Vector3.Lerp(transform.position, _oldPosition, Time.deltaTime * _moveSpeed);
                yield return null;
            }

            transform.position = _oldPosition;
            _isPressed = false;
            yield return null;
        }
    }
}
