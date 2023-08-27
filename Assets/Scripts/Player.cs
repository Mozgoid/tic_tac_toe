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
    public AIPlayer(Board.Symbol symbol) : base(symbol)
    {
    }
}

public class RemotePlayer : Player
{
    public RemotePlayer(Board.Symbol symbol) : base(symbol)
    {
    }
}

