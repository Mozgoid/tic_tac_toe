using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI statusText;

    [Zenject.Inject] private Match match;

    private void Awake()
    {
        match.OnGameOver += OnGameOver;
        match.OnPlayerSwitch += OnPlayerSwitch;
    }

    private void OnDestroy()
    {
        match.OnGameOver -= OnGameOver;
        match.OnPlayerSwitch -= OnPlayerSwitch;
    }

    private void OnPlayerSwitch(Player player)
    {
        statusText.text = $"{player.Name} place {player.Symbol}";
    }

    private void OnGameOver(Board.Symbol winner)
    {
        if (winner == Board.Symbol.None)
        {
            statusText.text = "Draw";
        }
        else
        {
            foreach (var player in match.Players)
            {
                if (player.Symbol == winner)
                {
                    statusText.text = $"{player.Name} wins";
                    return;
                }
            }
        }
    }

}
