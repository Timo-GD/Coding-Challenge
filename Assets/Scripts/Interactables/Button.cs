using System.Collections;
using CodingChallenge.Interactable;
using UnityEngine;

public class Button : InteractableObject
{
    private Vector3 _oldPosition;
    private Vector3 _targetPosition;
    private float _moveSpeed = 5f;
    private bool _isPressed;
    public override bool Use()
    {
        if (_isPressed)
            return false;

        _oldPosition = transform.position;

        _targetPosition = transform.position;
        _targetPosition.y -= 0.15f;

        _isPressed = true;
        StartCoroutine(GoToPosition());
        return true;
    }

    private IEnumerator GoToPosition()
    {
        while (Vector3.Distance(transform.position, _targetPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(ReturnToPosition());
    }

    private IEnumerator ReturnToPosition()
    {
        while (Vector3.Distance(transform.position, _oldPosition) > 0.0005f)
        {
            transform.position = Vector3.Lerp(transform.position, _oldPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        _isPressed = false;
        yield return null;
    }
}
