using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);//현재 state의 Tick 사용
    }
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        newState.Enter();
    }
}
