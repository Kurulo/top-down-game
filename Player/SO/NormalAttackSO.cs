using UnityEngine;

[CreateAssetMenu(menuName = "Player/Attacks/Normal Attack", fileName ="Normal Attack")]
public class NormalAttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOverrider;
    public float damage;
}
