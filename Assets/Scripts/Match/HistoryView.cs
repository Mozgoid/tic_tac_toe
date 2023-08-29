using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{
    [SerializeField] private Button button;

    private Match match;
    private History history;

    [Zenject.Inject]
    public void Inject(Match match, History history)
    {
        this.match = match;
        this.history = history;
    }

    public bool CanUndo => history.CanUndo
            && !match.IsGameOver
            && match.CurrentPlayer is LocalPlayer;

    public void Update()
    {
        button.interactable = CanUndo;
    }

    public void Undo()
    {
        if (!CanUndo)
        {
            Debug.Log("Can't undo right now");
            return;
        }

        history.Undo();
    }
}
