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

    public Match(Board board)
    {
        Board = board;
        Players = new Player[]
        {
            new LocalPlayer(Board.Symbol.X),
            new LocalPlayer(Board.Symbol.O),
        };
    }

    public Player[] Players { get; private set; }
    private int currentPlayerIndex = 0;
    public Player CurrentPlayer => Players[currentPlayerIndex];

    public Board.Symbol? Winner { get; private set; }

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
        if (Winner != null)
            throw new System.ArgumentException("Game already finished");

        if (player != CurrentPlayer)
            throw new System.ArgumentException("Not your turn");

        Board.Set(player.Symbol, pos.x, pos.y);

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
            Debug.Log($"Player {player.Name} wins!");
        }
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Length;
    }
}
