using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchAudio : MonoBehaviour
{
    [SerializeField] private AudioSource win, lose, tap, draw;

    [Zenject.Inject] private Match match;

    private void Start()
    {
        match.OnGameOver += OnGameOver;
        match.OnMoveMade += OnMoveMade;
    }

    private void OnDestroy()
    {
        match.OnGameOver -= OnGameOver;
        match.OnMoveMade -= OnMoveMade;
    }

    private void OnGameOver(Board.Symbol winner)
    {
        var hasLocalPlayers = false;
        foreach (var p in match.Players)
        {
            var isLocal = p is LocalPlayer;
            hasLocalPlayers |= isLocal;

            if (p.Symbol == winner && isLocal)
            {
                win.Play();
                return;
            }
        }

        if (hasLocalPlayers && winner != Board.Symbol.None)
        {
            lose.Play();
        }
        else
        {
            //else draw or ai match
            draw.Play();
        }
    }

    private void OnMoveMade(Board.Symbol symbol, Vector2Int pos)
    {
        tap.Play();
    }
}
