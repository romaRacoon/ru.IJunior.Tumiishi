using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stones;
    [SerializeField] private GameObject[] _spawnPositions;

    public event UnityAction<Stone> Spawned;

    private void Start()
    {
        Spawn(_stones.Count);
    }

    private void Spawn(int count)
    {
        int stoneIndex = 0;

        for (int i = 0; i < count; i++)
        {
            var stone = Instantiate(_stones[stoneIndex], _spawnPositions[i].transform.position, Quaternion.identity);
            Spawned?.Invoke(stone.GetComponent<Stone>());
            stoneIndex++;
        }
    }
}
