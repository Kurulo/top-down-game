using UnityEngine;
using Zenject;

public class AbilitiesInstaller : MonoInstaller
{
    [Header("Container for Abilities")]
    [SerializeField] private Transform _playerAbilitiesContainer;

    [Header("Abilities Prefabs")]
    [SerializeField] private CatchyHook _catchyHookPrefab;

    public override void InstallBindings()
    {
        BindCatchyHook();
    }

    private void BindCatchyHook()
    {
        CatchyHook catchyHook = Container.
            InstantiatePrefabForComponent<CatchyHook>(_catchyHookPrefab, _playerAbilitiesContainer);
            
        Container.Bind<CatchyHook>().FromInstance(catchyHook).AsSingle();
    }
}