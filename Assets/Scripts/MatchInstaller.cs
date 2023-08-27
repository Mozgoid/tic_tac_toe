using UnityEngine;
using Zenject;

public class MatchInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().NonLazy();
        Container.Bind<Match>().AsSingle().NonLazy();
    }
}
