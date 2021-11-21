using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MazeGenerator
{
    private CellModel[,] _cells;

    public CellModel[,] CreateSquareMaze(int size)
    {
        CreateCells(size, size);
        SidewinderAlgorithm(size, size);

        return _cells;
    }

    private void CreateCells(int rows, int columns)
    {
        _cells = new CellModel[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                _cells[i, j] = new CellModel();
                _cells[i, j]._wallState = WallStates.Up | WallStates.Right;

                if (i == rows - 1)
                {
                    _cells[i, j]._wallState |= WallStates.Down;
                }
                if (j == 0)
                {
                    _cells[i, j]._wallState |= WallStates.Left;
                }
            }
        }
    }

    private void SidewinderAlgorithm(int rows, int columns)
    {
        var random = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            List<CellModel> cellsInRow = new List<CellModel>();

            for (int j = 0; j < columns; j++)
            {
                cellsInRow.Add(_cells[i, j]);

                bool onUpSideBoud = i == 0;
                bool onRightSideBound = j == columns - 1;

                bool removeUpWall = onRightSideBound || (!onUpSideBoud && random.Next(2) == 0);

                if (onUpSideBoud && onRightSideBound)
                {
                    continue;
                }

                if (removeUpWall == false)
                {
                    _cells[i, j]._wallState &= ~WallStates.Right;
                }
                else
                {
                    int cellsInRowCount = cellsInRow.Count;
                    cellsInRow[random.Next(cellsInRowCount)]._wallState &= ~WallStates.Up;
                    cellsInRow.Clear();
                }
            }
        }
    }
}

[Flags]
public enum WallStates
{
    Right = 1,
    Up = 2,
    Left = 4,
    Down = 8
}

public class CellModel
{
    public WallStates _wallState;
}
