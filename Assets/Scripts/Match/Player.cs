using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; private set; }
    public Board.Symbol Symbol { get; private set; }

    public Player(Board.Symbol symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }
}

public class LocalPlayer : Player
{
    public LocalPlayer(Board.Symbol symbol, string name) : base(symbol, name)
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

    private const float MoveDelay = 0.9f;
    public CoroutineRunner coroutineRunner;
    public Difficulty difficulty = Difficulty.Easy;
    private Match match;
    public AIPlayer(Board.Symbol symbol, Match match, Difficulty difficulty, string name, CoroutineRunner coroutineRunner) : base(symbol, name)
    {
        this.match = match;
        this.difficulty = difficulty;
        this.coroutineRunner = coroutineRunner;
        match.OnPlayerSwitch += OnPlayerSwitch;
    }

    public void OnPlayerSwitch(Player player)
    {
        if (player == this)
        {
            coroutineRunner.StartCoroutine(MakeMoveCoroutine());
        }
    }

    IEnumerator MakeMoveCoroutine()
    {
        yield return new WaitForSeconds(MoveDelay);

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

// TODO: Implement this class
// Should wait for a remote move and then call match.MakeMove
public class RemotePlayer : Player
{
    public RemotePlayer(Board.Symbol symbol, string name) : base(symbol, name)
    {
    }
}

