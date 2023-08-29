using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple class to change scene.
/// Also Restart button use it to make random player order on restart
/// </summary>
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
