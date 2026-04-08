using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private bool _useDummyConfig;
    [SerializeField] private ScriptableGameConfig _scriptableConfig;

    public override void InstallBindings()
    {
        if (_useDummyConfig)
        {
            Container.Bind<IGameConfig>().To<DummyConfig>().AsSingle();
            Debug.Log("<color=yellow>DI: Используется Dummy конфигурация</color>");
        }
        else
        {
            Container.Bind<IGameConfig>()
                .To<RemoteConfigLoader>()
                .AsSingle()
                .WithArguments(_scriptableConfig);

            Debug.Log("<color=green>DI: Используется конфигурация из Scriptable Object</color>");
        }
    }
}