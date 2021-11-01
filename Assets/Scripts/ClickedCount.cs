using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickedCount : MonoBehaviour
{
    [SerializeField] private Game _game;

    private int _amount = 0;
    private Stone _targetStone;

    public int Amount => _amount;
    public Stone TargetStone => _targetStone;

    public event UnityAction AddedAmount;
    public event UnityAction FirstAddedAmount;
    public event UnityAction LastAddedAmount;

    public void AddAmount()
    {
        _amount++;
        if (_amount == 1)
        {
            FirstAddedAmount?.Invoke();
        }
        AddedAmount?.Invoke();

        if (_amount == 4)
        {
            LastAddedAmount?.Invoke();
        }
    }

    public void SetTargetStone(Stone stone)
    {
        _targetStone = stone;
    }

    public Transform GetTargetStoneHighestPoint()
    {
        return _targetStone.HighestPoint.transform;
    }

    public void ResetAmount()
    {
        _amount = 0;
    }
}
