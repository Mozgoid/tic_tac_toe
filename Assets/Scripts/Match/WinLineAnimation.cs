using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinLineAnimation : MonoBehaviour
{
    [Zenject.Inject] private Match match;
    [Zenject.Inject] private BoardView boardView;
    [Zenject.Inject] private Board board;

    [SerializeField] private SpriteRenderer winLine;

    private void Start()
    {
        winLine.gameObject.SetActive(false);
        match.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        match.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(Board.Symbol winner)
    {
        if (winner == Board.Symbol.None)
        {
            return;
        }

        winLine.transform.localRotation = Quaternion.identity;
        winLine.gameObject.SetActive(true);
        winLine.color = new Color(0, 0, 0, 0);
        winLine.DOColor(Color.black, 1.0f);

        var (dimension, _, wintype) = board.GetWinInfo();

        if (wintype == Board.WinType.Horizontal)
        {
            var middleCell = boardView.CellAt(new Vector2Int(board.Size / 2, dimension));
            winLine.transform.position = middleCell.transform.position;
        }
        else if (wintype == Board.WinType.Vertical)
        {
            var middleCell = boardView.CellAt(new Vector2Int(dimension, board.Size / 2));
            winLine.transform.position = middleCell.transform.position;
            winLine.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (wintype == Board.WinType.DiagonalBottomLeftToTopRight)
        {
            winLine.transform.localRotation = Quaternion.Euler(0, 0, 45);
        }
        else if (wintype == Board.WinType.OtherDiagonal)
        {
            winLine.transform.localRotation = Quaternion.Euler(0, 0, -45);
        }
    }
}
