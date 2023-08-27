using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    public Board.Symbol CurrentTurn { get; private set; }
    public Board.Symbol Winner { get; private set; }

    public void OnCellClick(Vector2Int pos)
    {
        Debug.Log($"Cell Clicked {pos}");
    }
}
