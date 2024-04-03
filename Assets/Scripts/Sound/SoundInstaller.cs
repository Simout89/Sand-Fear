using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SoundController>().FromComponentInHierarchy().AsSingle();
    }
}