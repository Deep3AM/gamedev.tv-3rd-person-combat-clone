using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;//힘이 한번만 작용하게
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);   
        float normalizedTime = GetNormalizedTime();
        if(normalizedTime>=previousFrameTime&&normalizedTime<1f)//공격을 시작함
        {
            if(normalizedTime>=attack.ForceTime)//공격을 하면서
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)//연속 공격시
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget!=null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }
    public override void Exit()
    {

    }
    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;//콤보의 끝
        if (normalizedTime < attack.ComboAttackTime) return;
        stateMachine.SwitchState
            (
            new PlayerAttackingState(stateMachine, attack.ComboStateIndex)
            );
    }
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward*attack.Force);//공격의 강도에 따라서 앞으로 더감
        alreadyAppliedForce = true;
    }
    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
        if(stateMachine.Animator.IsInTransition(0)&&nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;//다음 공격의 정보
        }
        else if(!stateMachine.Animator.IsInTransition(0)&&currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
