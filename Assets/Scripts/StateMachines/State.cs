using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);//일정 주기마다 반복하기 위해서
    public abstract void Exit();
}
