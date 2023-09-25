using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private Vector3 momentum = Vector3.zero;
    private const float CrossFadeDuration = 0.1f;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        if(stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();//땅에 닿으면 상태 돌아오게함
        }
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    }
    private void HandleLedgeDetect(Vector3 ledgeForward, Vector3 closestPoint)
    {
        Debug.Log("dfdf");
        stateMachine.SwitchState(new PlayerHangingState(stateMachine,ledgeForward, closestPoint));
    }
}
