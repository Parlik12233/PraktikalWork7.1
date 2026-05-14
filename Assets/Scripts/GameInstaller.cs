using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private bool _useDummyConfig;

    public override void InstallBindings()
    {
        if (_useDummyConfig)
        {
            Container.Bind<IGameConfig>().To<DummyConfig>().AsSingle();

            Container.Bind<PlayerConfig>().AsSingle().NonLazy();

            Debug.Log("<color=yellow>DI: Для игры используется Dummy конфигурация. Облачный конфиг скачается в фоновом режиме.</color>");
        }
        else
        {
            Container.BindInterfacesAndSelfTo<PlayerConfig>().AsSingle().NonLazy();

            Debug.Log("<color=green>DI: Для игры используется асинхронная конфигурация (JSON)</color>");
        }
    }
}