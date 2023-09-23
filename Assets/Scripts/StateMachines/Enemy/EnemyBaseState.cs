using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);//물리효과는 받지만 안 움직임
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FacePlayer()//플레이어를 보며 쫓게
    {
        if (stateMachine.Player == null)
            return;
        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.IsDead) return false;//플레이어가 죽으면 행동하지 않음
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr < stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
}
