using UnityEngine;


public class GroundEnemyMovement
{
    private ContainerForEnemyComponents m_components;

    public GroundEnemyMovement(ContainerForEnemyComponents context) {
        m_components = context;
    }

    public void Move(float slowingFactor = 1f) {
        m_components.NavMesh.speed = m_components.MeleeEnemySettings.MovingSpeed / slowingFactor;
        m_components.NavMesh.SetDestination(m_components.PlayerTransform.position);

        FaceToTarget();
    }

    public void FaceToTarget()
    {
        Vector3 direction = (m_components.PlayerTransform.position - m_components.SelfTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        float smoothSpeed = Time.deltaTime * m_components.MeleeEnemySettings.TurnSpeed;
        Quaternion calculatedRotation = Quaternion.Slerp(m_components.SelfTransform.rotation, lookRotation, smoothSpeed);

        m_components.SelfTransform.rotation = calculatedRotation;
    }
}
