using System.Collections;
using UnityEngine;


public class RangeEnemy : Enemy {
    private ContainerForEnemyComponents m_components;

    private EnemyHealthDetector m_healthDetector;
    private MeleeEnemyAttack m_attack;

    private GroundEnemyMovement m_movement;

    public RangeEnemy(ContainerForEnemyComponents components) {
        m_components = components;

        m_movement = new GroundEnemyMovement(m_components);
        m_healthDetector = new EnemyHealthDetector(m_components);
        m_attack = new MeleeEnemyAttack(m_components);

        m_components.EventReceiver.OnAttack.AddListener(Attack);
    }

    public override void Aproaching() {
        m_movement.Move(3);
    }

    public override void Attack() {
        
    }

    public override void Chasing() {
        m_movement.Move();
    }

    public override void Idle() {
        
    }

    public override void Injured() {
        
    }

    public override void Patrol() {
        m_movement.Move(2);
    }
}
