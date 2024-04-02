using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public GameObject Player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInteract>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CarController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SoundController>().FromComponentInHierarchy().AsSingle();
        Container.BindInstance(Player).AsSingle();
    }
}