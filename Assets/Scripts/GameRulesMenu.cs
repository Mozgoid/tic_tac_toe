using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulesMenu : MonoBehaviour
{
    [Zenject.Inject] private MatchSettings matchSettings;

 
    [SerializeField] TMPro.TextMeshProUGUI difficulty, timer, mode, symbol;

    [SerializeField] private Button easy, medium, hard;
    [SerializeField] private Button x, o, random;
    [SerializeField] private Button pvp, pve, eve;


    private void Start()
    {
        x.onClick.AddListener(() => ChangeSymbol(Board.Symbol.X));
        o.onClick.AddListener(() => ChangeSymbol(Board.Symbol.O));
        random.onClick.AddListener(() => ChangeSymbol(Board.Symbol.None));

        easy.onClick.AddListener(() => ChangeDifficulty(AIPlayer.Difficulty.Easy));
        medium.onClick.AddListener(() => ChangeDifficulty(AIPlayer.Difficulty.Medium));
        hard.onClick.AddListener(() => ChangeDifficulty(AIPlayer.Difficulty.Hard));

        pvp.onClick.AddListener(() => ChangeGameMode(MatchSettings.GameMode.LocalMultiPlayer));
        pve.onClick.AddListener(() => ChangeGameMode(MatchSettings.GameMode.SinglePlayerVsAI));
        eve.onClick.AddListener(() => ChangeGameMode(MatchSettings.GameMode.AIvsAI));

        SyncWithSettings();
    }



    public void ChangeDifficulty(AIPlayer.Difficulty difficulty)
    {
        matchSettings.Difficulty = difficulty;
        SyncWithSettings();
    }

    public void ChangeTurnTime(float turnTime)
    {
        matchSettings.TurnTime = (int)turnTime;
        SyncWithSettings();
    }

    public void ChangeGameMode(MatchSettings.GameMode gameMode)
    {
        matchSettings.Mode = gameMode;
        SyncWithSettings();
    }

    public void ChangeSymbol(Board.Symbol symbol)
    {
        matchSettings.FirstPlayerSymbol = symbol;
        SyncWithSettings();
    }

    private void SyncWithSettings()
    {
        difficulty.text = $"Difficulty: {matchSettings.Difficulty}";
        timer.text = $"Turn Time: {matchSettings.TurnTime}";
        mode.text = $"Mode: {matchSettings.Mode}";

        var symbolName = matchSettings.FirstPlayerSymbol == Board.Symbol.None ? "Random" : matchSettings.FirstPlayerSymbol.ToString();
        symbol.text = $"Symbol: {symbolName}";
    }
}
