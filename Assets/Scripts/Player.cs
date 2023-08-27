using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; private set; }
    public Board.Symbol Symbol { get; private set; }

    public Player(Board.Symbol symbol)
    {
        Symbol = symbol;
        Name = $"Player {symbol}";
    }
}

public class LocalPlayer : Player
{
    public LocalPlayer(Board.Symbol symbol) : base(symbol)
    {
    }
}

public class AIPlayer : Player
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
    }

    public Difficulty difficulty = Difficulty.Easy;
    private Match match;
    public AIPlayer(Board.Symbol symbol, Match match, Difficulty difficulty) : base(symbol)
    {
        this.match = match;
        this.difficulty = difficulty;
        match.OnPlayerSwitch += OnPlayerSwitch;
    }

    public void OnPlayerSwitch(Player player)
    {
        if (player == this)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    match.MakeMove(this, Hint.RandomHint(match.Board));
                    break;
                case Difficulty.Medium:
                    match.MakeMove(this, Hint.MediumHint(match.Board));
                    break;
                case Difficulty.Hard:
                    match.MakeMove(this, Hint.SmartHint(match.Board, Symbol));
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}

public class RemotePlayer : Player
{
    public RemotePlayer(Board.Symbol symbol) : base(symbol)
    {
    }
}

