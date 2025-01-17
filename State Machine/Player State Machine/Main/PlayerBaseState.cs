
public abstract class PlayerBaseState
{
    private PlayerStateMachine _context;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;

    private bool _isRootState = false;

    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Context { get { return _context; } }
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void InitializeSubState();
    public abstract void CheackSwitchState();
    public void UpdateStates() 
    {
        UpdateState();

        if (_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }
    protected void SwitchState(PlayerBaseState newState) 
    {
        ExitState();

        newState.EnterState();

        if (_isRootState)
        {
            _context.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    } 

    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
