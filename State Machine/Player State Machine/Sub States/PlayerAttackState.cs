using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float _lastClickedTime;
    private int _attackCounter = 0;

    private PlayerAttack _playerAttack;

    private bool _click = false;
    private bool _clicked = false;

    public PlayerAttackState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory)
    {

    }

    public override void EnterState()
    {
        if (_playerAttack == null) _playerAttack = new PlayerAttack(Context);

        EventSubscriber();
        RotateToTheClickPoint();

        Context.PlayerAnimator.SetBool(PlayerAnimationHashed.IsAttack, true);
        Context.PlayerAnimator.Play(PlayerAnimationHashed.Attack, 0, 0f);
    }

    public override void UpdateState()
    {
        if (Time.time - _lastClickedTime >= 0.9f)
        {
            _clicked = false;
        }

        ExitAttack();
        CheackSwitchState();
    }

    private void ExitAttack()
    {
        if (Context.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !_clicked)
        {
            Context.IsAttack = false;
        }
    }

    private void AttackProcces()
    {
        if (Time.time - _lastClickedTime >= 0.2f)
        {
            _clicked = true;

            if (Context.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                RotateToTheClickPoint();

                Context.PlayerAnimator.runtimeAnimatorController = Context.PlayerSettings.Combo[_attackCounter].animatorOverrider;
                Context.PlayerAnimator.Play(PlayerAnimationHashed.Attack, 0, 0f);

                _attackCounter++;

                if (_attackCounter >= Context.PlayerSettings.Combo.Count)
                {
                    _attackCounter = 0;
                }
            }

            _lastClickedTime = Time.time;
        }
    }

    private void MouseClickStarted()
    {
        if (!_click)
            AttackProcces();
        _click = true;
    }

    private void MouseClickCanceled()
    {
        _click = false;
    }

    public override void CheackSwitchState()
    {
        if (!Context.IsAttack && !Context.IsRunning)
        {
            SwitchState(Factory.Idle());
        }
        else if (!Context.IsAttack && Context.IsRunning)
        {
            SwitchState(Factory.Run());
        }
    }

    private void RotateToTheClickPoint()
    {
        Ray ray = Context.MainCamera.ScreenPointToRay(Context.MouseClickPosition);
        Plane groundPlane = new Plane(Vector3.up, Context.SelfTransform.position);

        float rayLenght;

        if (groundPlane.Raycast(ray, out rayLenght))
        {
            CalculateRotation(ray, rayLenght);
        }
    }

    private void CalculateRotation(Ray ray, float rayLenght)
    {
        Vector3 pointToLook = ray.GetPoint(rayLenght);

        Vector3 neededPoint = new Vector3(pointToLook.x, Context.SelfTransform.position.y, pointToLook.z);
        Context.SelfTransform.LookAt(neededPoint);
    }

    public override void ExitState()
    {
        _attackCounter = 0;

        EventUnsubscribe();

        Context.PlayerAnimator.SetBool(PlayerAnimationHashed.IsAttack, false);
    }

    private void EventSubscriber()
    {
        Context.AnimatorEvents.OnAttack.AddListener(_playerAttack.Attack);

        Context.Input.Player.Attack.started += _ => MouseClickStarted();
        Context.Input.Player.Attack.canceled += _ => MouseClickCanceled();
    }

    private void EventUnsubscribe()
    {
        Context.AnimatorEvents.OnAttack?.AddListener(_playerAttack.Attack);

        Context.Input.Player.Attack.started -= _ => MouseClickStarted();
        Context.Input.Player.Attack.canceled -= _ => MouseClickCanceled();
    }

    public override void InitializeSubState()
    {

    }
}

