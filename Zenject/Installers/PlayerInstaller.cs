using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [Header("Spawn Point from Start")]
    [SerializeField] private Transform _spawnPoint;

    [Header("Main player")]
    [SerializeField] private GameObject _playerPrefab;

    public override void InstallBindings()
    {
        BindPlayerPrefab();
    }

    private void BindPlayerPrefab()
    {
        PlayerStateMachine mainPlayer = Container.
            InstantiatePrefabForComponent<PlayerStateMachine>(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);

        Container.Bind<PlayerStateMachine>().FromInstance(mainPlayer).AsSingle();
    }
}