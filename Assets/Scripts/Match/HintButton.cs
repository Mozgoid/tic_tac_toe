using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    private Match match;
    private Board board;
    private BoardView boardView;

    [Zenject.Inject]
    public void Inject(Match match, Board board, BoardView boardView)
    {
        this.match = match;
        this.board = board;
        this.boardView = boardView;
    }

    public void OnClick()
    {
        if (match.IsGameOver)
        {
            Debug.Log("Can't hint. Game is over");
            return;
        }

        if (!(match.CurrentPlayer is LocalPlayer))
        {
            Debug.Log("Wait for you turn");
            return;
        }

        var hint = Hint.SmartHint(board, match.CurrentPlayer.Symbol);
        Debug.Log($"{match.CurrentPlayer.Name} should play {hint.x}, {hint.y}");

        var cell = boardView.CellAt(hint);
        cell.OnHint();
    }
}
