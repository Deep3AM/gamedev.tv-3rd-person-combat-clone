using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private Vector3 momentum = new Vector3();
    private const float CrossFadeDuration = 0.1f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);//�����ϴ� �� �����ֱ�
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;//�̵��ϰ� �ִ� ������ �����
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        if(stateMachine.Controller.velocity.y<=0f)
        {
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));//�����ϴ� ���� ���ϸ� ����������
        }
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    }

    private void HandleLedgeDetect(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closestPoint));
    }
}
