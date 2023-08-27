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

    private readonly Symbol[,] board = new Symbol[3, 3];

    public int Size => board.GetLength(0);

    public void Reset()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++) 
                board[i, j] = Symbol.None;
        }
    }

    public void SetBoard(Symbol[,] newBoard)
    {
        if (newBoard.GetLength(0) != Size || newBoard.GetLength(1) != Size)
            throw new System.ArgumentException($"Board must be {Size}x{Size}");

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++) 
                this.board[i, j] = newBoard[i, j];
        }
    }

    public void Set(Symbol symbol, int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
            throw new System.ArgumentException("Invalid coordinates");

        if (board[x, y] != Symbol.None)
            throw new System.ArgumentException("Position already taken");

        board[x, y] = symbol;
    }

    public Symbol Get(int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
            throw new System.ArgumentException("Invalid coordinates");

        return board[x, y];
    }

    public Symbol WhoWins()
    {
        // horizontal check
        for (int i = 0; i < Size; i++)
        {
            if (board[i, 0] != Symbol.None && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return board[i, 0];
        }

        // vertical check
        for (int j = 0; j < Size; j++)
        {
            if (board[0, j] != Symbol.None && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                return board[0, j];
        }

        // diagonal check
        if (board[0, 0] != Symbol.None)
        {
            for (int i = 1; i < Size; i++)
            {
                if (board[i, i] != board[0, 0])
                    break;

                if (i == Size - 1)
                    return board[0, 0];
            }
        }

        // other diagonal check
        if (board[0, Size-1] != Symbol.None)
        {
            for (int i = 1; i < Size; i++)
            {
                if (board[i, Size - 1 - i] != board[0, Size-1])
                    break;

                if (i == Size - 1)
                    return board[0, Size-1];
            }
        }

        return Symbol.None;
    }

    public bool IsFull()
    {
        foreach (Symbol symbol in board)
        {
            if (symbol == Symbol.None)
                return false;
        }

        return true;
    }
}
