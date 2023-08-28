using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    private Match match;
    private Board board;

    [Zenject.Inject]
    public void Inject(Match match, Board board)
    {
        this.match = match;
        this.board = board;
    }
    
    struct Move
    {
        public Vector2Int position;
        public Board.Symbol symbol;
    }

    private Stack<Move> moves = new Stack<Move>();

    private void Start()
    {
        board.OnSymbolChange += OnSymbolChange;
    }

    private void OnDestroy()
    {
        board.OnSymbolChange -= OnSymbolChange;
    }

    private void OnSymbolChange(Board.Symbol symbol, Vector2Int pos)
    {
        if (symbol == Board.Symbol.None)
        {
            return;
        }
        moves.Push(new Move { position = pos, symbol = symbol });
    }

    public void Undo()
    {
        if (match.IsGameOver)
        {
            Debug.Log("Can't undo. Game is over");
            return;
        }

        if (!(match.CurrentPlayer is LocalPlayer))
        {
            Debug.Log("Can't undo. Not your turn");
            return;
        }

        if (moves.Count < 2)
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
