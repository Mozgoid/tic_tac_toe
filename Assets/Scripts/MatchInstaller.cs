using UnityEngine;
using Zenject;

public class MatchInstaller : MonoInstaller
{
    [SerializeField] private BoardView boardView;

    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<Match>().AsSingle().NonLazy();
        Container.Bind<BoardView>().FromInstance(boardView).AsSingle();
    }
}
