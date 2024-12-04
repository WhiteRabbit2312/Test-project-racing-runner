using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    public override void InstallBindings()
    {
        Container.Bind<PlayerSpawner>().AsSingle();
    }
}
