using UnityEngine;
using Zenject;


public class RangeEnemyStateMachine : StateMachine
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
        _components = new ContainerForEnemyComponents(_settings, this, _audioManager, _player.transform);

        _components.Enemy = new RangeEnemy(_components);

        State patrollState     = new RangeEnemyPatrollState(_components);
        State chasingState     = new MeleeEnemyChasingState(_components);
        State aproachState     = new MeleeEnemyAproachState(_components);
        State checkState       = new RangeEnemyCheckState(_components);
        State lostTarget       = new RangeEnemyLostTargetState(_components);
        State attackState      = new RangeEnemyAttackState(_components);
        State rangeAttackState = new RangeEnemyRangeAttackState(_components);

        Transition isCanCheck           = new IsCanCheckTarget(this.transform, _player.transform, _components);
        Transition checkTimer           = new TimerTransition(7f);
        Transition inChasingRange       = new InChasingRange(this.transform, _player.transform, _components);
        Transition outOfChasingRange    = new OutOfChasingRange(this.transform, _player.transform, _components);
        Transition inAproachRange       = new InAproachingRange(this.transform, _player.transform, _components);
        Transition inAttackRange        = new InAttackRange(this.transform, _player.transform, _components);
        Transition inRangeAttackRange   = new InRangeAttackRange(this.transform, _player.transform, _components);

        Init(patrollState, new()
        {
            {rangeAttackState, new ()
            {
                { inChasingRange, chasingState},
                { inAproachRange, aproachState}
            } },
            {attackState, new () 
            {
                { inChasingRange,    chasingState },
                { inAproachRange,    aproachState },
                { inRangeAttackRange, rangeAttackState },
                { outOfChasingRange, patrollState }
            } },
            {lostTarget, new ()
            {
                { inChasingRange,    chasingState },
                { isCanCheck,        checkState   }
            } },
            {checkState, new ()
            {
                { checkTimer,        patrollState },
                { inChasingRange,    chasingState },
                { inAproachRange,    aproachState }
            } },
            {patrollState, new ()
            {
                { inChasingRange,    chasingState },
                { inAproachRange,    aproachState }
            } },
            {chasingState, new ()
            {
                { outOfChasingRange,  lostTarget   },
                { inRangeAttackRange, rangeAttackState },
                { inAproachRange,     aproachState }
            } },
            { aproachState, new ()
            {
                { inChasingRange,    chasingState },
                { inRangeAttackRange, rangeAttackState },
                { inAttackRange,     attackState  }
            } }
        });
    }
}
