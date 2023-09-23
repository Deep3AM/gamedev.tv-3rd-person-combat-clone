using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);//Å¸°ÙÀ» ¾ø¾ÚÀ¸·Î½á Å¸°ÙÆÃ »óÅÂ¿¡¼­ ¹þ¾î³²
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
