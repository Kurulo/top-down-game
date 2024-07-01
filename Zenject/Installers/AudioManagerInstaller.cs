using UnityEngine;
using Zenject;

public class AudioManagerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _audioManager;

    public override void InstallBindings()
    {
        BindAudioManagerSingleton();
    }

    private void BindAudioManagerSingleton()
    {
        AudioManager audioManager = 
            Container.InstantiatePrefabForComponent<AudioManager>(_audioManager);

        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
    }
}