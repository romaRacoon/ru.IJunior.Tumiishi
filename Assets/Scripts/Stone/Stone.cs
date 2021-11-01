using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stone : MonoBehaviour
{
    private readonly Vector3 _boxColliderSizeConst = new Vector3(1, -0.07f, 1);
    private readonly Vector3 _boxColliderCenterConst = new Vector3(0, -0.06f, 0);
    private readonly int _bottomPointIndexConst = 2;

    [SerializeField] private Transform[] _points;

    private bool _isHighest = false;
    private bool _isClicked = false;
    private Rigidbody _rigidbody;
    private Vector3 _startBottomPointPosition;

    public bool IsHighest => _isHighest;
    public bool IsClicked => _isClicked;
    public Transform HighestPoint => _points[1];
    public Transform BottomPoint => _points[2];

    private void Start()
    {
        _points = GetComponentsInChildren<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _startBottomPointPosition = _points[2].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            SetIsKinematicToTrue();
            GetComponent<Relocation>().StartGoToStartPosition();
        }
    }

    public void EditIsClickedToTrue()
    {
        _isClicked = true;
    }

    public void EditIsClickedToFalse()
    {
        _isClicked = false;
    }

    public void EditIsHighest()
    {
        _isHighest = true;
    }

    public void CreateBoxColliderInBottomPoint()
    {
        var boxCollider = _points[_bottomPointIndexConst].gameObject.AddComponent<BoxCollider>();
        boxCollider.size = _boxColliderSizeConst;
        boxCollider.center = _boxColliderCenterConst;
    }

    public void SetParent()
    {
        _points[2].SetParent(null);
        gameObject.transform.SetParent(_points[2], true);
    }

    public void SetIsKinematicToFalse()
    {
        _rigidbody.isKinematic = false;
    }

    public void SetIsKinematicToTrue()
    {
        _rigidbody.isKinematic = true;
    }

    public void ResetFields()
    {
        gameObject.transform.SetParent(null);
        _points[2].transform.SetParent(gameObject.transform, true);
        _isClicked = false;
        _isHighest = false;

        _points[2].position = _startBottomPointPosition;

        var relocation = GetComponent<Relocation>();
        relocation.enabled = true;

        if (_points[2].TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
        {
            Destroy(_points[2].GetComponent<BoxCollider>());
        }
    }
}
