using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerSoundsToggle
{
    private PlayerStateMachine _context;
    private AudioManager _audio;

    private string _stepSoundName = "PlayerStep";

    public PlayerSoundsToggle(PlayerStateMachine context, AudioManager audio)
    {
        _context = context;
        _audio = audio;

        SetEventSubscribers();
    }

    private void SetStepSound()
    {
        if (_context.CollidingSurface != null)
        {
            if (_context.CollidingSurface.CompareTag("Wood"))
            {
                _stepSoundName = "PlayerStepWood";
            }
            else if (_context.CollidingSurface.CompareTag("Stone"))
            {
                _stepSoundName = "PlayerStep";
            }
            else
            {
                _stepSoundName = "PlayerStep";
            }
        }
        else
        {
            _stepSoundName = "PlayerStep";
        }
    }

    private void PlayerStepSounds()
    {
        SetStepSound();
        _audio.PlaySound(_stepSoundName);
    }

    private void PlayerAttackSound()
    {
        _audio.PlaySound("PlayerAttack");
        _audio.PlaySound("PlayerAttackVoice");
    }

    private void SetEventSubscribers()
    {
        _context.AnimatorEvents.OnStep.AddListener(PlayerStepSounds);
        _context.AnimatorEvents.OnAttack.AddListener(PlayerAttackSound);
    }
}
