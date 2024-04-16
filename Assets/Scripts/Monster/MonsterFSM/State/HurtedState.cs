using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtedState : MonsterState
{
    const string Hurted = "root|Anim_monster_scavenger_gethit";
    public HurtedState(MonsterFSMContext _context) : base(_context)
    {
    }
    public override void EnterState()
    {
        context.Agent.isStopped=true;
        context.Agent.SetDestination(context.Transform.position);
        context.Agent.velocity = Vector3.zero;
        context.AudioSource.PlayHurtedSound();
        context.Animator.Play(Hurted,-1,0f);
    }

    public override void ExitState()
    {
        context.Agent.isStopped = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }

    public override void OnTriggerStay(Collider other)
    {
    }

    public override void StateUpdateWork()
    {
        if (context.Animator.GetCurrentAnimatorStateInfo(0).IsName(Hurted))
        {
            if (context.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (context.Target != null)
                {
                    ChangeStateTo(Monster.MonsterState.Move);
                }
                else
                {
                    ChangeStateTo(Monster.MonsterState.Idle);
                }
            }
        }
    }
}
