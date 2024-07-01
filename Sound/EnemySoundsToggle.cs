using System;

public class EnemySoundsToggle
{
    private ContainerForEnemyComponents _components;
    private AudioManager _audioManager;

    public EnemySoundsToggle(ContainerForEnemyComponents components, AudioManager audioManager)
    {
        _audioManager = audioManager;
        _components = components;

        SubscribeToEvents();
    }

    private void StepSounds()
    {
        _audioManager.PlaySound("EnemyStep");
        _audioManager.PlaySound("EnemyStepBack");
    }

    private void SubscribeToEvents()
    {
        _components.EventReceiver.OnStep.AddListener(StepSounds);
    }
}

