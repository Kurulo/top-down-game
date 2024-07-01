using System.Collections;
using UnityEngine;


public class PlayerAttack
{
    private PlayerStateMachine _player;

    public PlayerAttack(PlayerStateMachine player)
    {
        _player = player;
    }

    public void Attack()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        Vector3 start = _player.SelfTransform.position;
        Vector3 boxSize = new Vector3(15f, 2.5f, 5f) / 2f;
        Vector3 direction = _player.SelfTransform.forward;

        float distance = 10f;

        HitEnemys(start, boxSize, direction, distance);
        HitDestroyables(start, boxSize, direction, distance);
    }

    private void HitEnemys(Vector3 start, Vector3 boxSize, Vector3 direction, float distance)
    {
        RaycastHit[] hits = Physics.BoxCastAll(start, boxSize, direction, Quaternion.identity, distance, _player.PlayerSettings.EnemyLayer);

        if (hits.Length <= 0) { return; }

        foreach (RaycastHit hit in hits)
        {
            StateMachine enemy = hit.transform.GetComponent<StateMachine>();

            if (enemy.Componets.Health.IsAlive())
            {
                enemy.Componets.Health.DecreaseHealth(_player.PlayerSettings.Damage);
            }
        }
    }

    private void HitDestroyables(Vector3 start, Vector3 boxSize, Vector3 direction, float distance)
    {
        RaycastHit[] hits = Physics.BoxCastAll(start, boxSize, direction, Quaternion.identity, distance, _player.PlayerSettings.DestroyableLayer);

        if (hits.Length <= 0) { return; }

        foreach (RaycastHit hit in hits)
        {
            Destroyable destroyable = hit.transform.GetComponent<Destroyable>();
            destroyable.Detected();
        }
    }
}
