using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void ResestCoordinates()
    {
        transform.position = _startPosition;
    }
}
