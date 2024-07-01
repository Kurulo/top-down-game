using UnityEngine;


public class DistanceToTarget
{
    private Transform m_target;
    private float m_distance;

    public DistanceToTarget(Transform target) {
        m_target = target;
    }

    public void CalculateDistanceToTraget(Vector3 selfV3) {
        m_distance = Vector3.Distance(selfV3, m_target.position);
    }

    public bool IsTargetInRange(float range) {
        if (m_distance <= range) {
            return true;
        } else { return false; }
    }
}
