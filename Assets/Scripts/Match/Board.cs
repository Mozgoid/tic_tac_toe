using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class holds information about which symbol is present on board.
/// It also can check win condition.
/// First dimension is X (from left to right), second is Y (from bottom to top).
/// </summary>
public class Board
{
    public enum Symbol
    {
        None,
        X,
        O,
    }

    public enum WinType
    {
        None,
        Draw,
        Horizontal,
        Vertical,
        DiagonalBottomLeftToTopRight,
        OtherDiagonal,
    }

    private readonly Symbol[,] board = new Symbol[3, 3];
    public Action<Symbol, Vector2Int> OnSymbolChange;

    public int Size => board.GetLength(0);

    public void Reset()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                SetWithoutCheck(Symbol.None, x, y);
            }
        }
    }

    public void SetBoard(Symbol[,] newBoard)
    {
        if (newBoard.GetLength(0) != Size || newBoard.GetLength(1) != Size)
            throw new System.ArgumentException($"Board must be {Size}x{Size}");

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                SetWithoutCheck(newBoard[x, y], x, y);
            }
        }
    }

    public void Set(Symbol symbol, int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
            throw new System.ArgumentException("Invalid coordinates");

        if (board[x, y] != Symbol.None)
            throw new System.ArgumentException("Position already taken");

        SetWithoutCheck(symbol, x, y);
    }

    public void SetWithoutCheck(Symbol symbol, int x, int y)
    {
        board[x, y] = symbol;
        OnSymbolChange?.Invoke(symbol, new Vector2Int(x, y));
    }

    public Symbol Get(int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
            throw new System.ArgumentException("Invalid coordinates");

        return board[x, y];
    }

    public bool WillSymbolWinIfMoveHere(Symbol symbol, int x, int y)
    {
        if (Get(x, y) != Symbol.None)
            throw new System.ArgumentException("Position already taken");

        board[x, y] = symbol;
        var result = WhoWins() == symbol;
        board[x, y] = Symbol.None;
        return result;
    }

    public Symbol? WhoWins()
    {
        var (dimension, symbol, wintype) = GetWinInfo();
        if (wintype == WinType.None)
            return null;
        return symbol;
    }

    public (int dimension, Symbol symbol, WinType wintype) GetWinInfo()
    {
        // horizontal check
        for (int x = 0; x < Size; x++)
        {
            if (board[x, 0] != Symbol.None && board[x, 0] == board[x, 1] && board[x, 1] == board[x, 2])
            {
                return (x , board[x, 0], WinType.Horizontal);
            }
        }

        // vertical check
        for (int y = 0; y < Size; y++)
        {
            if (board[0, y] != Symbol.None && board[0, y] == board[1, y] && board[1, y] == board[2, y])
            {
                return (y , board[0, y], WinType.Vertical);
            }
        }

        // diagonal check
        if (board[0, 0] != Symbol.None)
        {
            for (int i = 1; i < Size; i++)
            {
                if (board[i, i] != board[0, 0])
                    break;

                if (i == Size - 1)
                {
                    return (0 , board[0, 0], WinType.DiagonalBottomLeftToTopRight);
                }
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
                {
                    return (0 , board[0, Size-1], WinType.OtherDiagonal);
                }
            }
        }

        if (IsFull())
            return (0, Symbol.None, WinType.Draw);

        return (0, Symbol.None, WinType.None);
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
