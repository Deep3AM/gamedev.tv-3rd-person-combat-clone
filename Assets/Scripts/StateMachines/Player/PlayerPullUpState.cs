using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");
    private readonly Vector3 Offset = new Vector3(0f, 2.325f, 0.65f);//오르는 정도
    private const float CrossFadeDuration = 0.1f;
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator,"Climbing")<1f) return;//애니메이션이 안끝나면 이 동작 안끝냄
        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(Offset,Space.Self);
        stateMachine.Controller.enabled = true;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);//가해지는 방향이 없도록.
        stateMachine.ForceReceiver.Reset();
    }


}
