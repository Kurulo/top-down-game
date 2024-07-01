using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventReceiver : MonoBehaviour{
    [HideInInspector]
    public UnityEvent OnShoot;
    [HideInInspector]
    public UnityEvent OnAttack;
    [HideInInspector]
    public UnityEvent OnStep;
    [HideInInspector]
    public UnityEvent OnEndAnimation;
    [HideInInspector]
    public UnityEvent OnDeath;

    
    public void OnShootEvent() {
        OnShoot?.Invoke();
    }

    public void OnAttackEvent() {
        OnAttack?.Invoke();
    }

    public void OnStepEvent() {
        OnStep?.Invoke();
    }

    public void OnEndAnimationEvent() {
        OnEndAnimation?.Invoke();
    }

    public void OnDeathEvent() {
        OnDeath?.Invoke();
    }
}
