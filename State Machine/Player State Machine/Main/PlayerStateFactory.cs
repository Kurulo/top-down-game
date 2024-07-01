using System;
using System.Collections.Generic;

enum PlayerMachineStates
{
    idle,
    run,
    fall,
    sprint,
    ground,
    attack
}

public class PlayerStateFactory
{
    private PlayerStateMachine _context;
    private Dictionary<PlayerMachineStates, PlayerBaseState> _statesCashed = new Dictionary<PlayerMachineStates, PlayerBaseState>();

    public PlayerStateFactory (PlayerStateMachine context)
    {
        _context = context;

        SetDictionaryItem();
    }

    private void SetDictionaryItem()
    {
        _statesCashed[PlayerMachineStates.idle] = new PlayerIdleState(_context, this);
        _statesCashed[PlayerMachineStates.run] = new PlayerRunState(_context, this);
        _statesCashed[PlayerMachineStates.fall] = new PlayerFallState(_context, this);
        _statesCashed[PlayerMachineStates.sprint] = new PlayerSprintState(_context, this);
        _statesCashed[PlayerMachineStates.ground] = new PlayerGroundState(_context, this);
        _statesCashed[PlayerMachineStates.attack] = new PlayerAttackState(_context, this);
    }

    public PlayerBaseState Idle()
    {
        return _statesCashed[PlayerMachineStates.idle];
    }

    public PlayerBaseState Run()
    {
        return _statesCashed[PlayerMachineStates.run];
    }

    public PlayerBaseState Fall() 
    {
        return _statesCashed[PlayerMachineStates.fall];
    }

    public PlayerBaseState Sprint()
    {
        return _statesCashed[PlayerMachineStates.sprint];
    }

    public PlayerBaseState Ground()
    {
        return _statesCashed[PlayerMachineStates.ground];
    }

    public PlayerBaseState Attack()
    {
        return _statesCashed[PlayerMachineStates.attack];
    }
}
