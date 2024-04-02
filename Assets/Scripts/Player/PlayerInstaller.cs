using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public GameObject Player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInteract>().FromComponentInHierarchy().AsSingle();
        Container.BindInstance(Player).AsSingle();
    }
}