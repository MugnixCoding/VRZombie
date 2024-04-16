using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFSMContext
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;
    private StateMachineCoroutineController coroutineController;
    private MonsterAudioSource audioSource;

    public Transform Target;

    private Transform attackDetect;

    public MonsterFSMContext(Transform _transform, Animator _animator, NavMeshAgent navMeshAgent, Transform _attackDetect,
        StateMachineCoroutineController _coroutineController, MonsterAudioSource _audioSource)
    {
        transform = _transform;
        animator = _animator;
        agent = navMeshAgent;
        attackDetect = _attackDetect;
        coroutineController = _coroutineController;
        audioSource = _audioSource;
        Target = null;
    }
    public Transform Transform => transform;
    public Animator Animator => animator;
    public NavMeshAgent Agent => agent;
    public Transform AttackDetect => attackDetect;
    public StateMachineCoroutineController CoroutineController => coroutineController;
    public MonsterAudioSource AudioSource => audioSource;
}
