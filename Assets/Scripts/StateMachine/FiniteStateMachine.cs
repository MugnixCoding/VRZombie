using System;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine<EState> where EState:Enum
{
    public Dictionary<EState, BaseState<EState>> states;
    //protected Queue<EState> scheduledStates;
    protected BaseState<EState> currentState;
    public BaseState<EState> CurrentState => currentState;
    protected bool stateLock=false;
    public FiniteStateMachine()
    {
        states = new Dictionary<EState, BaseState<EState>>();
        //scheduledStates = new Queue<EState>();
        currentState = null;

    }
    public void UpdateWork()
    {
        if (currentState!=null && !stateLock)
        {
            currentState.StateUpdateWork();
        }
    }
    public void AddState(EState newStateName,BaseState<EState> newState)
    {
        states.Add(newStateName, newState);
        states[newStateName].switchStateTo += TransitionToState;
    }
    public void TransitionToState(EState nextState)
    {
        stateLock = true;
        if (currentState !=null)
        {
            currentState.ExitState();
        }
        currentState = states[nextState];
        if (currentState != null)
        {
            currentState.EnterState();
        }
        stateLock = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (currentState!=null)
        {
            currentState.OnTriggerEnter(other);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (currentState != null)
        {
            currentState.OnTriggerStay(other);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (currentState != null)
        {
            currentState.OnTriggerExit(other);
        }
    }
}
