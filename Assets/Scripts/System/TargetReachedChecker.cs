using System;
using UnityEngine;
using UnityEngine.Events;

public class TargetReachedChecker : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float threshhold = 0.02f;
    public UnityEvent OnTargetReached;

    private bool wasReached = false;
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position,target.position);
        if (distance<threshhold&&!wasReached)
        {
            OnTargetReached.Invoke();
            wasReached = true;
        }
        else if (distance >= threshhold) 
        {
            wasReached = false;
        }
    }
}
