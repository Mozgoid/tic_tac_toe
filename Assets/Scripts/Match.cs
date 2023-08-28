using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


/// <summary>
/// The main class of the game logic.
/// It holds information about the board, current turn, players. Checks timer and win conditions.
/// </summary>
public class Match
{
    public Board Board { get; private set; }
    public MatchSettings Settings { get; private set; }

    public Match(Board board, MatchSettings settings)
    {
        Board = board;
        Settings = settings;
    }

    public void StartMatch()
    {
        var firstPlayerSymbol = Settings.FirstPlayerSymbol;
        if (firstPlayerSymbol == Board.Symbol.None)
            firstPlayerSymbol = UnityEngine.Random.value < 0.5f ? Board.Symbol.X : Board.Symbol.O;
        var secondPlayerSymbol = firstPlayerSymbol == Board.Symbol.X ? Board.Symbol.O : Board.Symbol.X;

        Player player1, player2;

        switch (Settings.Mode)
        {
            case MatchSettings.GameMode.SinglePlayerVsAI:
                player1 = new LocalPlayer(firstPlayerSymbol, "Player 1");
                player2 = new AIPlayer(secondPlayerSymbol, this, Settings.Difficulty, "Bot 1");
                break;
            case MatchSettings.GameMode.LocalMultiPlayer:
                player1 = new LocalPlayer(firstPlayerSymbol, "Player 1");
                player2 = new LocalPlayer(secondPlayerSymbol, "Player 2");
                break;
            case MatchSettings.GameMode.AIvsAI:
                player1 = new AIPlayer(firstPlayerSymbol, this, Settings.Difficulty, "Bot 1");
                player2 = new AIPlayer(secondPlayerSymbol, this, Settings.Difficulty, "Bot 2");
                break;
            default:
                throw new System.ArgumentException("Invalid game mode");
        }

        if (firstPlayerSymbol == Board.Symbol.O)
        {
            var temp = player1;
            player1 = player2;
            player2 = temp;
        }

        Players = new Player[]
        {
            player1,
            player2,
        };
        NextPlayer();
    }

    public Player[] Players { get; private set; }
    private int currentPlayerIndex = -1;
    public Player CurrentPlayer {get; private set;}

    // null if not decided yet, None if draw, otherwise the winner
    public Board.Symbol? Winner { get; private set; }
    public bool IsGameOver => Winner != null;

    public Action<Player> OnPlayerSwitch;

    public void OnCellClick(Vector2Int pos)
    {
        if (!(CurrentPlayer is LocalPlayer))
            return;

        if (Board.Get(pos.x, pos.y) != Board.Symbol.None)
            return;

        MakeMove(CurrentPlayer, pos);
    }

    public void MakeMove(Player player, Vector2Int pos)
    {
        if (IsGameOver)
            throw new System.ArgumentException("Game already finished");

        if (player != CurrentPlayer)
            throw new System.ArgumentException("Not your turn");

        Board.Set(player.Symbol, pos.x, pos.y);
        Debug.Log($"{player.Name} set {player.Symbol} at {pos}");

        Winner = Board.WhoWins();
        if (Winner == null)
        {
            NextPlayer();
        }
        else if (Winner == Board.Symbol.None)
        {
            Debug.Log("Draw!");
        }
        else
        {
            Debug.Log($"{player.Name} wins!");
        }
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Length;
        CurrentPlayer = Players[currentPlayerIndex];
        OnPlayerSwitch?.Invoke(CurrentPlayer);
    }
}
