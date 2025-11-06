using System.Collections;
using CodingChallenge.Interactable;
using UnityEngine;
using UnityEngine.Rendering;

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
        _targetPosition = Vector3.zero;

        _isPressed = true;
        StartCoroutine(GoToPosition());
        return true;
    }

    private IEnumerator GoToPosition()
    {
        while (transform.position.y >= _targetPosition.y)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        Debug.Log("Cehek");
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(ReturnToPosition());
    }

    private IEnumerator ReturnToPosition()
    {
        yield return new WaitForSeconds(2f);
        while (transform.position.y <= _oldPosition.y)
        {
            transform.position = Vector3.Lerp(transform.position, _oldPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        _isPressed = false;
        yield return null;
    }
}
