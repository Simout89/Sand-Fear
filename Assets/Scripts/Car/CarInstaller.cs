using Zenject;

public class CarInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CarController>().FromComponentInHierarchy().AsSingle();
    }
}