﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;

    private Board board;

    private CellView[,] cells;

    private void Start()
    {
        SetBoard(new Board());
    }

    public void SetBoard(Board board)
    {
        this.board = board;

        if (cells != null)
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
        }

        cells = new CellView[board.Size, board.Size];

        var cellSize = cellPrefab.Size;
        var cellBottomLeftStart = new Vector3(-cellSize.x * board.Size / 2, -cellSize.y * board.Size / 2, 0);

        Debug.Log(cellSize);

        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                var cell = Instantiate(cellPrefab, transform);
                cell.transform.position = cellBottomLeftStart + new Vector3(cellSize.x * i, cellSize.y * j, 0);
                cell.Position = new Vector2Int(i, j);
                // cell.OnClick += OnCellClicked;
                cells[i, j] = cell;
            }
        }
    }
}
