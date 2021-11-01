using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private ClickedCount _clickedCount;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Ground _ground;
    [SerializeField] private Transform _stonesFirstTarget;

    private List<Stone> _stones = new List<Stone>();
    private List<Relocation> _relocations = new List<Relocation>();
    private List<Stone> _tower = new List<Stone>();

    private void OnEnable()
    {
        _clickedCount.AddedAmount += OnAddedAmount;
        _clickedCount.FirstAddedAmount += OnFirstAddedAmount;
        _clickedCount.LastAddedAmount += OnLastAddedAmount;

        _spawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _clickedCount.AddedAmount -= OnAddedAmount;
        _clickedCount.FirstAddedAmount -= OnFirstAddedAmount;
        _clickedCount.LastAddedAmount -= OnLastAddedAmount;

        _spawner.Spawned -= OnSpawned;
    }

    private void Start()
    {
        _settingsMenu.SetActive(false);
    }

    private void OnAddedAmount()
    {
        var newTarget = FindNewTarget();

        for (int i = 0; i < _relocations.Count; i++)
        {
            _relocations[i].SetNewTarget(newTarget);
        }

        _tower.Add(GetLastStoneBuild());
        _clickedCount.SetTargetStone(_tower.Last());

        if (_clickedCount.Amount == 1)
        {
            _tower.RemoveAt(_tower.Count - 1);
        }
    }

    private void OnFirstAddedAmount()
    {
        Stone stone = GetFirstPartOfTower();
        stone.CreateBoxColliderInBottomPoint();
        stone.SetParent();
        _tower.Add(stone);
    }

    private Stone GetFirstPartOfTower()
    {
        for (int i = 0; i < _relocations.Count; i++)
        {
            if (_relocations[i].GetComponent<Stone>().IsClicked)
            {
                return _relocations[i].GetComponent<Stone>();
            }
        }

        return null;
    }

    private Transform FindNewTarget()
    {
        for (int i = 0; i < _relocations.Count; i++)
        {
            if (_relocations[i].GetComponent<Stone>().IsClicked)
            {
                return _relocations[i].GetComponent<Stone>().HighestPoint;
            }
        }

        return null;
    }

    private void OnSpawned(Stone stone)
    {
        _stones.Add(stone);
        _relocations.Add(stone.GetComponent<Relocation>());

        var relocation = stone.GetComponent<Relocation>();
        relocation.SetClickedCount(_clickedCount);
        relocation.SetNewTarget(_stonesFirstTarget);
        relocation.SetStartTarget(_stonesFirstTarget);
    }

    private void OnLastAddedAmount()
    {
        for (int i = 0; i < _stones.Count; i++)
        {
            _stones[i].SetIsKinematicToFalse();
        }
    }

    private Stone GetLastStoneBuild()
    {
        for (int i = 0; i < _relocations.Count; i++)
        {
            if(_relocations[i].GetComponent<Stone>().IsHighest==true && _relocations[i].GetComponent<Stone>().IsClicked == false)
            {
                return _relocations[i].GetComponent<Stone>();
            }
        }

        return null;
    }

    public void OpenSettings()
    {
        _settingsMenu.SetActive(true);
        _exitButton.interactable = false;
        _settingsButton.interactable = false;
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        _settingsMenu.SetActive(false);
        _settingsButton.interactable = true;
        _exitButton.interactable = true;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
