using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineCoroutineController : MonoBehaviour
{
    private Dictionary<IEnumerator, Coroutine> coroutineSet = new Dictionary<IEnumerator, Coroutine>();
    public void StartStateCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
    public void StopStateCoroutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
}
