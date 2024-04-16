using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public delegate void DelegateSwitchStateTo(EState nextState);
    public DelegateSwitchStateTo switchStateTo;
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void StateUpdateWork();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    public abstract void OnTriggerExit(Collider other);
    protected void ChangeStateTo(EState nextState)
    {
        switchStateTo?.Invoke(nextState);
    }
}
