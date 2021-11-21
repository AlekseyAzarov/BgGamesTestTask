using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _northWall;
    [SerializeField] private GameObject _southWall;
    [SerializeField] private GameObject _westWall;
    [SerializeField] private GameObject _eastWall;

    [SerializeField] private GameObject _interactableZoneObj;
    [SerializeField] private GameObject _playerStartObj;

    public void Init(CellModel cellModel)
    {
        _northWall.SetActive(cellModel._wallState.HasFlag(WallStates.Up));
        _southWall.SetActive(cellModel._wallState.HasFlag(WallStates.Down));
        _westWall.SetActive(cellModel._wallState.HasFlag(WallStates.Left));
        _eastWall.SetActive(cellModel._wallState.HasFlag(WallStates.Right));

        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted += OnRestart;
    }

    public void SetInteractableZoneType<T>() where T : AbstractInteractableZone
    {
        _interactableZoneObj.AddComponent<T>();
        _interactableZoneObj.SetActive(true);
    }

    public void SetPlayerStartCell()
    {
        _playerStartObj.SetActive(true);
    }

    private void OnRestart()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted -= OnRestart;
    }
}
