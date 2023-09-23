using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDuration = 0.1f;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage,stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator)>=1)//공격 애니메이션이 끝나면 다시 쫓는 상태로 바꿈
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        FacePlayer();//공격을 할때도 돌게하기
    }
    public override void Exit()
    {
    }
}
