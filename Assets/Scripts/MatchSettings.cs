using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSettings
{
    public AIPlayer.Difficulty Difficulty { get; set; } = AIPlayer.Difficulty.Medium;

    // None means assign randomly
    public Board.Symbol FirstPlayerSymbol = Board.Symbol.None;

    public float TurnTime {get; set;} = 5f;

    public enum GameMode
    {
        SinglePlayerVsAI,
        LocalMultiPlayer,
        AIvsAI,
    }

    public GameMode Mode { get; set; } = GameMode.SinglePlayerVsAI;
}
