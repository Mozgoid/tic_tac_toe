using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Zenject.Inject] private MatchSettings matchSettings;

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void SetRandomSymbolsAndChangeScene()
    {
        matchSettings.FirstPlayerSymbol = Board.Symbol.None;
        ChangeScene();
    }
}
