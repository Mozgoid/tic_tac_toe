﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class tracks move history and helps undo them
/// </summary>
public class History
{
    private Board board;

    public History(Board board)
    {
        this.board = board;
        board.OnSymbolChange += OnSymbolChange;
    }
    
    struct Move
    {
        public Vector2Int position;
        public Board.Symbol symbol;
    }

    private Stack<Move> moves = new Stack<Move>();


    private void OnSymbolChange(Board.Symbol symbol, Vector2Int pos)
    {
        if (symbol == Board.Symbol.None)
        {
            return;
        }
        moves.Push(new Move { position = pos, symbol = symbol });
    }

    public bool CanUndo => moves.Count >= 2;

    public void Undo()
    {
        if (!CanUndo)
        {
            Debug.Log("Can't undo. Not enough moves to undo");
            return;
        }

        var move = moves.Pop();
        board.SetWithoutCheck(Board.Symbol.None, move.position.x, move.position.y);
        move = moves.Pop();
        board.SetWithoutCheck(Board.Symbol.None, move.position.x, move.position.y);
    }
}
