using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Customization customization;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
        Container.Bind<Customization>().FromInstance(customization).AsSingle();
        Container.Bind<MatchSettings>().AsSingle().NonLazy();
    }
}
