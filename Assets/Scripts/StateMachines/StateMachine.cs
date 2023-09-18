using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);//���� state�� Tick ���
    }
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        newState.Enter();
    }
}
