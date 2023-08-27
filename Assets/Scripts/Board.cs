using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public enum Symbol
    {
        None,
        X,
        O,
    }

    public Symbol[,] board = new Symbol[3, 3];

    public void Reset()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++) 
                board[i, j] = Symbol.None;
        }
    }

    public Symbol WhoWins()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (board[i, 0] != Symbol.None && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return board[i, 0];
        }

        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (board[0, j] != Symbol.None && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                return board[0, j];
        }

        if (board[0, 0] != Symbol.None && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return board[0, 0];

        if (board[0, 2] != Symbol.None && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return board[0, 2];

        return Symbol.None;
    }
}
