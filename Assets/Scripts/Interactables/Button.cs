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
        while (transform.position.y > _targetPosition.y)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        yield return null;
    }

    // private IEnumerator ReturnToPosition()
    // {
    //     yield return new WaitForSeconds(2f);
    //     _isPressed = false;
    //     float elapsedTime = 0;
    //     while (elapsedTime < _changeTime)
    //     {
    //         transform.position = Vector3.Lerp(transform.position, _oldPosition, elapsedTime / _changeTime);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //     yield return null;
    // }
}
