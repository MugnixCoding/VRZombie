using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachineCoroutineController))]
public class Monster : PoolObject,IDamageable
{
    [SerializeField] private int Health = 10;
    [SerializeField] private float deathTime = 5f;
    [SerializeField] private CapsuleCollider hitCollider;

    private int currentHealth;
    private bool isFSMInitialize;
    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Hurted,
        Dead
    }
    [SerializeField]
    private MonsterAudioSource audioSource;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform attackDetect;

    private NavMeshAgent agent;
    private FiniteStateMachine<MonsterState> monsterFSM;
    private MonsterFSMContext context;
    private StateMachineCoroutineController coroutineController;

    void Awake()
    {
        hitCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        audioSource.audioSource = GetComponent<AudioSource>();
        coroutineController = GetComponent<StateMachineCoroutineController>();
        monsterFSM = new FiniteStateMachine<MonsterState>();
        context = new MonsterFSMContext(transform, animator, agent, attackDetect, coroutineController, audioSource);
        isFSMInitialize = false;
    }

    void Update()
    {
        monsterFSM.UpdateWork();
    }
    private void InitFSM()
    {
        isFSMInitialize = true;
        monsterFSM.AddState(MonsterState.Idle, new IdleState(context));
        monsterFSM.AddState(MonsterState.Move, new MoveToTargetState(context));
        monsterFSM.AddState(MonsterState.Attack, new AttackTargetState(context));
        monsterFSM.AddState(MonsterState.Hurted, new HurtedState(context));
        monsterFSM.AddState(MonsterState.Dead, new DeadState(context));
    }
    public override void StartActive()
    {
        if (!isFSMInitialize)
        {
            InitFSM();
        }
        currentHealth = Health;
        hitCollider.enabled = true;
        agent.isStopped = false;
        monsterFSM.TransitionToState(MonsterState.Idle);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            monsterFSM.TransitionToState(MonsterState.Dead);
            StartCoroutine(Dead());
        }
        else
        {
            monsterFSM.TransitionToState(MonsterState.Hurted);
        }
    }
    public void TakeDamage(int damage,Transform attacker)
    {
        TakeDamage(damage);
    }
    private IEnumerator Dead()
    {
        if (hitCollider!=null)
        {
            hitCollider.enabled = false;
        }
        yield return new WaitForSeconds(deathTime);
        DisableObject(EventArgs.Empty);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
