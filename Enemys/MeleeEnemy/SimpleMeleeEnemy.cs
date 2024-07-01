using UnityEngine;

public class SimpleMeleeEnemy : Enemy
{
    private ContainerForEnemyComponents m_components;

    private EnemyHealthDetector m_healthDetector;
    private MeleeEnemyAttack m_attack;

    private GroundEnemyMovement m_movement;

    public SimpleMeleeEnemy(ContainerForEnemyComponents components) {
        m_components = components;

        m_movement = new GroundEnemyMovement(m_components);
        m_healthDetector = new EnemyHealthDetector(m_components);
        m_attack = new MeleeEnemyAttack(m_components);

        m_components.EventReceiver.OnDeath.AddListener(DeathProcess);
        m_components.EventReceiver.OnAttack.AddListener(Attack);
    }

    //******** Movement ************

    public override void Idle() {
        
    }

    public override void Aproaching() {
        m_movement.Move(3f);
    }

    public override void Chasing() {
        m_movement.Move();
    }

    public override void Patrol() {
    
    }

    //******** Fighting ************

    public override void Attack() {
        m_attack.Attack();
    }

    public override void Injured() {
        m_healthDetector.HealthBarVizualithation();
    }


    //******** Life System ************

    private void DeathProcess() {
        m_healthDetector.Death(m_components.Ragdoll);
    }
}
