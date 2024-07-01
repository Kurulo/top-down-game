using UnityEngine;
using Zenject;

public class MeleeEnemyStateMachine : StateMachine
{
    [SerializeField] private EnemySettings _settings;

    private PlayerStateMachine _player;
    private AudioManager _audioManager;

    private ContainerForEnemyComponents _componentContainer;
    public ContainerForEnemyComponents Components { get { return _componentContainer; } }

    [Inject] 
    private void Construct(PlayerStateMachine player, AudioManager audioManager)
    {
        _player = player;
        _audioManager = audioManager;
    }

    private void Awake()
    {
        _components = new ContainerForEnemyComponents(_settings, this, _audioManager, _player.transform) ;

        _components.Enemy = new SimpleMeleeEnemy(_components);

        State idleState = new MeleeEnemyIdleState(_components);
        State chasingState = new MeleeEnemyChasingState(_components);
        State aproachState = new MeleeEnemyAproachState(_components);
        State attackState = new MeleeEnemyAttackState(_components);
        State injuredState = new MeleeEnemyInjuredState(_components);
        State deathState = new MeleeEnemyDeathState(_components);

        Transition inChasingRange = new InChasingRange(transform, _player.transform, _components);
        Transition outOfChasingRange = new OutOfChasingRange(transform, _player.transform, _components);
        Transition inAproachingRange = new InAproachingRange(transform, _player.transform, _components);
        Transition inAttackRange = new InAttackRange(transform, _player.transform, _components);
        Transition isInjured = new IsInjured(_components);
        Transition isDead = new IsDead(_components);

        Init(idleState, new()
        {
            { injuredState, new()
            {
                { isDead, deathState},
                { inAttackRange, attackState },
                { inAproachingRange, aproachState },
                { inChasingRange, chasingState },
                { outOfChasingRange, idleState }
            } },

            { attackState,  new() {
                { isDead, deathState},
                { isInjured, injuredState },
                { inAproachingRange, aproachState },
                { inChasingRange, chasingState },
                { outOfChasingRange, idleState }
            } },

            { aproachState, new() {
                { inChasingRange, chasingState },
                { inAttackRange, attackState },
                { isInjured, injuredState }
            } },

            { chasingState, new() {
                { outOfChasingRange, idleState },
                { inAproachingRange, aproachState},
                { isInjured, injuredState }
            } },

            { idleState,    new() {
                { inAproachingRange, aproachState },
                { inChasingRange, chasingState },
                { isInjured, injuredState }
            } } 
        });
    }
}
