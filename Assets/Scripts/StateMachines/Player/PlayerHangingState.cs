using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 closestPoint;
    private Vector3 ledgeForward;
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private const float CrossFadeDuration = 0.1f;
    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward,Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation=Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.Controller.enabled = false;
        stateMachine.transform.position=closestPoint-(stateMachine.LedgeDetector.transform.position-stateMachine.transform.position);//가장 가까운 위치에서 내 손의 트리거 위치에서 내 위치를 빼주는 offset을 줌으로써 벽에 손이 닿게함
        stateMachine.Controller.enabled = true;
        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        if (stateMachine.InputReader.MovementValue.y<0f)
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }
    
    public override void Exit()
    {

    }
}
