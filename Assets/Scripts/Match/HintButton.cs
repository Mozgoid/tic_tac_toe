using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    private Match match;
    private Board board;

    [Zenject.Inject]
    public void Inject(Match match, Board board)
    {
        this.match = match;
        this.board = board;
    }

    public void OnClick()
    {
        if (match.IsGameOver)
        {
            Debug.Log("Can't hint. Game is over");
            return;
        }

        var hint = Hint.SmartHint(board, match.CurrentPlayer.Symbol);
        Debug.Log($"{match.CurrentPlayer.Name} should play {hint.x}, {hint.y}");
    }
}
