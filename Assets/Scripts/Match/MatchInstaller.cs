using UnityEngine;
using Zenject;

public class MatchInstaller : MonoInstaller
{
    [SerializeField] private BoardView boardView;
    [SerializeField] private Timer timer;
    [SerializeField] private CoroutineRunner coroutineRunner;

    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<Match>().AsSingle().NonLazy();
        Container.Bind<History>().AsSingle().NonLazy();
        Container.Bind<BoardView>().FromInstance(boardView).AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
        Container.Bind<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
    }
}
