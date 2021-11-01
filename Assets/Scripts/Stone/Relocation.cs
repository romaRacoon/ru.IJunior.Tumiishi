using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ClickedCount _clickedCount;
    private Vector3 _touchOffSet;
    private float _touchZCoordinate;
    private Transform _transform;
    private float _speed = 10f;
    private Stone _stone;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Transform _startTarget;
    private Touch _touch;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startPosition = _transform.position;
        _startRotation = _transform.rotation;
    }

    private void OnMouseDown()
    {
        _touchZCoordinate = Camera.main.WorldToScreenPoint(_transform.position).z;
        _touchOffSet = _transform.position - GetTouchWorldPosition();
        _stone = GetComponent<Stone>();
        _stone.EditIsClickedToTrue();

        if (_clickedCount.Amount >= 1)
        {
            gameObject.GetComponent<Stone>().SetParent();
            _transform = GetComponent<Stone>().BottomPoint;
        }
    }

    private void OnMouseDrag()
    {
        if (enabled)
        {
            _transform.position = GetTouchWorldPosition() + _touchOffSet;
        }
    }

    private void OnMouseUp()
    {
        StartCoroutine(GoToTarget());
    }

    private Vector3 GetTouchWorldPosition()
    {
        Vector3 touchPosition = _touch.position;
        touchPosition.z = _touchZCoordinate;

        return Camera.main.ScreenToWorldPoint(touchPosition);
    }

    private IEnumerator GoToStartPosition()
    {
        _transform = GetComponent<Transform>();
        _transform.rotation = _startRotation;

        while (_transform.position != _startPosition)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _startPosition, _speed * Time.deltaTime);

            yield return null;
        }

        GetComponent<Stone>().ResetFields();
        ResetTarget();
        _clickedCount.ResetAmount();
    }

    private IEnumerator GoToTarget()
    {
        _stone.EditIsHighest();
        _stone.EditIsClickedToTrue();

        if (_clickedCount.Amount == 0)
        {
            while (_transform.position != _target.position)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            SetNewTarget(_clickedCount.GetTargetStoneHighestPoint());
            
            while (_transform.position != _target.position)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }

        enabled = false;
        _clickedCount.AddAmount();
        _clickedCount.SetTargetStone(_stone);
        _stone.EditIsClickedToFalse();

        yield break;
    }

    private void ResetTarget()
    {
        _target = _startTarget;
    }

    public void StartGoToStartPosition()
    {
        StartCoroutine(GoToStartPosition());
    }
    
    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void SetClickedCount(ClickedCount clickedCount)
    {
        _clickedCount = clickedCount;
    }

    public void SetStartTarget(Transform startTarget)
    {
        _startTarget = startTarget;
    }
}
