using System.Collections.Generic;
using UnityEngine;

public static class Hint
{
    private static List<Vector2Int> emptyPositionsCache;
    public static Vector2Int RandomHint(Board board)
    {
        if (board.IsFull())
            throw new System.ArgumentException("Board is full");

        if (emptyPositionsCache == null)
        {
            emptyPositionsCache = new List<Vector2Int>(board.Size * board.Size);
        }
        emptyPositionsCache.Clear();

        for (int x = 0; x < board.Size; ++x)
        {
            for (int y = 0; y < board.Size; ++y)
            {
                if (board.Get(x, y) == Board.Symbol.None)
                {
                    emptyPositionsCache.Add(new Vector2Int(x, y));
                }
            }
        }
        return emptyPositionsCache[Random.Range(0, emptyPositionsCache.Count)];
    }

    // Try center and corners first, then random
    public static Vector2Int MediumHint(Board board)
    {
        if (board.IsFull())
            throw new System.ArgumentException("Board is full");

        var center = new Vector2Int(board.Size / 2, board.Size / 2);
        if (board.Get(center.x, center.y) == Board.Symbol.None)
        {
            return center;
        }

        if (board.Get(0, 0) == Board.Symbol.None)
        {
            return new Vector2Int(0, 0);
        }
        if (board.Get(0, board.Size - 1) == Board.Symbol.None)
        {
            return new Vector2Int(0, board.Size - 1);
        }
        if (board.Get(board.Size - 1, 0) == Board.Symbol.None)
        {
            return new Vector2Int(board.Size - 1, 0);
        }
        if (board.Get(board.Size - 1, board.Size - 1) == Board.Symbol.None)
        {
            return new Vector2Int(board.Size - 1, board.Size - 1);
        }

        return RandomHint(board);
    }

    // Try to win, then try to block, then MediumHint
    public static Vector2Int SmartHint(Board board, Board.Symbol symbol)
    {
        if (board.IsFull())
            throw new System.ArgumentException("Board is full");

        var opponent = symbol == Board.Symbol.X ? Board.Symbol.O : Board.Symbol.X;

        Vector2Int? blockingMove = null;

        for (int x = 0; x < board.Size; ++x)
        {
            for (int y = 0; y < board.Size; ++y)
            {
                if (board.Get(x, y) == Board.Symbol.None)
                {
                    if (board.WillSymbolWinIfMoveHere(symbol, x, y))
                    {
                        return new Vector2Int(x, y);
                    }

                    if (blockingMove == null && board.WillSymbolWinIfMoveHere(opponent, x, y))
                    {
                        blockingMove = new Vector2Int(x, y);
                    }
                }
            }
        }

        if (blockingMove != null)
        {
            return blockingMove.Value;
        }

        return MediumHint(board);
    }

    //TODO: even better hint is possible. When smart hint can't detect wins or blocks,
    // It could try search position that can lead to win in 2 moves, if not blocked by opponent.
}
