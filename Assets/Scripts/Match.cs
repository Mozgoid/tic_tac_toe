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
    public Timer Timer { get; private set; }

    public Match(Board board, MatchSettings settings, Timer timer)
    {
        Board = board;
        Settings = settings;
        Timer = timer;
        Timer.OnNoMoreTime += OnNoMoreTime;
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
    public Action<Board.Symbol> OnGameOver;

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
        {
            Debug.Log("Game already finished");
            return;
        }

        if (player != CurrentPlayer)
            throw new System.ArgumentException("Not your turn");

        Board.Set(player.Symbol, pos.x, pos.y);
        Debug.Log($"{player.Name} set {player.Symbol} at {pos}");

        var winner = Board.WhoWins();
        if (winner == null)
        {
            NextPlayer();
        }
        else
        {
            GameOver(winner.Value);
        }
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Length;
        CurrentPlayer = Players[currentPlayerIndex];
        Debug.Log($"Next player is {CurrentPlayer.Name}");
        Timer.StartTurn(Settings.TurnTime);
        OnPlayerSwitch?.Invoke(CurrentPlayer);
    }

    private void OnNoMoreTime()
    {
        if (IsGameOver)
        {
            Debug.Log("Game already finished");
            return;
        }

        Debug.Log($"{CurrentPlayer.Name} ran out of time");
        GameOver(CurrentPlayer.Symbol == Board.Symbol.X ? Board.Symbol.O : Board.Symbol.X);
    }

    private void GameOver(Board.Symbol winner)
    {
        Winner = winner;
        Timer.StopTimer();
        Debug.Log($"Game over, winner is {winner}");
        OnGameOver?.Invoke(winner);
    }
}
