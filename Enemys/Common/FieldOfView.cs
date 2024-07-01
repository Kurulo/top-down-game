using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public EnemySettings Settings;
    public EnemyRanges Ranges;
    public LayerMask PlayerLayer;
    public LayerMask ObstacleMask;

    [Header("For editor")]
    public bool ShowStartRanges = true;

    [HideInInspector]
    public Transform Target = null;
    
    public bool PlayerInVisibleRange = false;

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay", 0.2f);
        Ranges = GetComponent<StateMachine>().Componets.Ranges;
        ShowStartRanges = false;
    }

    private IEnumerator FindTargetWithDelay (float delay)
    {
        while (true) 
        { 
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    private void FindVisibleTarget()
    {
        Collider[] targetInVisibleRadious = Physics.OverlapSphere(transform.position, Ranges.ChasingRange, PlayerLayer);

        for (int i = 0; i < targetInVisibleRadious.Length; i++)
        {
            Transform target = targetInVisibleRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < Settings.ViewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    Target = target;
                    PlayerInVisibleRange = true;
                }
                else
                {
                    Target = null;
                    PlayerInVisibleRange = false;
                }
            }
            else
            {
                Target = null;
                PlayerInVisibleRange = false;
            }
        }
    }

    public Vector3 DirFromAnle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
