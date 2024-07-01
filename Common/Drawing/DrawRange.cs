#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.AI;

[ExecuteAlways]
public class DrawRange : MonoBehaviour
{
    [SerializeField]
    private EnemySettings settings;

    [Header("Choose draw or not")]
    public bool Draw = false;

    private void OnDrawGizmosSelected()
    {
        if (Draw)
        {   
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, transform.up, settings.ChaseRange, 2f);


            Handles.color = new Color(255, 102, 0);
            Handles.DrawWireDisc(transform.position, transform.up, settings.AproachRange, 2f);

            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, settings.AttackRange, 2f);
        }
    }
}
#endif