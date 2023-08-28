using UnityEngine;
using Zenject;

public class MatchInstaller : MonoInstaller
{
    [SerializeField] private BoardView boardView;
    [SerializeField] private Timer timer;

    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<Match>().AsSingle().NonLazy();
        Container.Bind<BoardView>().FromInstance(boardView).AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
    }
}
