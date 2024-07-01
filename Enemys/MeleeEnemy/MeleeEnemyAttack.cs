using UnityEngine;

public class MeleeEnemyAttack
{
    private ContainerForEnemyComponents m_components;

    public MeleeEnemyAttack(ContainerForEnemyComponents components) {
        m_components = components;
    }

    public void Attack() {
        Vector3 start = m_components.SelfTransform.position;
        Vector3 boxSize = new Vector3(10f, 2.5f, 4f) / 2f;
        Vector3 direction = m_components.SelfTransform.forward;

        float distance = 10f;

        Quaternion orientation = m_components.SelfTransform.rotation;

        RaycastHit playerHit;
        PlayerStateMachine playerStateMachine;

        if (Physics.BoxCast(start, boxSize, direction, out playerHit, orientation, distance, m_components.MeleeEnemySettings.PlayerLayer))
        {
            playerStateMachine = playerHit.transform.GetComponent<PlayerStateMachine>();

            playerStateMachine.TakeDamage(m_components.MeleeEnemySettings.Damage);
        }
    }
}
