using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
    }
}