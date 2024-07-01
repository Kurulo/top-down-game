using UnityEngine;

public class EnemyHealthDetector
{
    private ContainerForEnemyComponents m_components;

    public EnemyHealthDetector(ContainerForEnemyComponents components) {
        m_components = components;
    }

    public void Death(RagdollSwitcher ragdoll) {
        m_components.SelfTransform.gameObject.layer = LayerMask.NameToLayer("Ragdoll");
        m_components.Animator.ResetTrigger(EnemyAnimationHashed.Death);

        ragdoll.OnRagdoll();

        m_components.SelfTransform.GetComponent<StateMachine>().enabled = false;
    }

    public void HealthBarVizualithation() {
        float procentCalculetion = m_components.Health.CurrentHp() / m_components.Health.MaxHealth();
        m_components.HealthBar.fillAmount = procentCalculetion;
    }
}
