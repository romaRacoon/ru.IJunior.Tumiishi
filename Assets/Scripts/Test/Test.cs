using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _prefabHighPoint;

    [SerializeField] private GameObject _prefabBottomPoint;

    private void Start()
    {
        //_bottomPoint.transform.position = _prefabHighPoint.transform.position;
        //gameObject.transform.position = _bottomPoint.transform.position;

        _prefabBottomPoint.transform.SetParent(null);//удаляет родителя
        gameObject.transform.SetParent(_prefabBottomPoint.transform, true);//делает родителем
        _prefabBottomPoint.transform.position = _prefabHighPoint.transform.position;
    }
}
