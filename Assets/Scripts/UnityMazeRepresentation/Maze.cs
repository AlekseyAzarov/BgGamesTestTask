using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maze : MonoBehaviour
{
    [SerializeField, Range(5, 50)] private int _mazeSize;
    [SerializeField, Range(0, 1)] private float _deadZoneGeneartionChance;
    [SerializeField] private Transform _cell;
    [SerializeField] NavMeshSurface _navMeshSurface;

    private MazeGenerator _mazeGenerator = new MazeGenerator();
    private float _cellOffset = 0.5f;

    private void Start()
    {
        Initialize();
        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted += Initialize;
    }

    public void Initialize()
    {
        _navMeshSurface.RemoveData();

        CellModel[,] cellModels = _mazeGenerator.CreateSquareMaze(_mazeSize);

        for (int i = 0; i < _mazeSize; i++)
        {
            for (int j = 0; j < _mazeSize; j++)
            {
                GameObject cellObj = Instantiate(_cell.gameObject);
                cellObj.transform.SetParent(transform, false);
                cellObj.transform.position = new Vector3(i - _cellOffset, 0, -j - _cellOffset);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.Init(cellModels[j, i]);

                if (i == 0 && j == _mazeSize - 1)
                {
                    cell.SetPlayerStartCell();
                    continue;
                }

                if (i == _mazeSize - 1 && j == 0)
                {
                    cell.SetInteractableZoneType<FinishZone>();
                    continue;
                }

                bool makeCellDeadZone = Random.Range(0f, 1f) <= _deadZoneGeneartionChance;

                if (makeCellDeadZone == true)
                {
                    cell.SetInteractableZoneType<DeadZone>();
                }
            }
        }

        StartCoroutine(BuildNavMeshDelayed());
    }

    private IEnumerator BuildNavMeshDelayed()
    {
        yield return new WaitForSeconds(0.5f);

        _navMeshSurface.BuildNavMesh();
    }

    private void OnDestroy()
    {
        GameDelegatesAndEvents.Instance.PlayerFinishedGameRestarted -= Initialize;
    }
}
