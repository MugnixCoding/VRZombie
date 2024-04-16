using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetState : MonsterState
{
    const string Walk = "root|Anim_monster_scavenger_walk";
    private float footstepTimer;
    private float attackRange = 0.8f;
    public MoveToTargetState(MonsterFSMContext _context) : base(_context)
    {
    }
    public override void EnterState()
    {
        context.Agent.SetDestination(context.Target.position);
        context.Animator.Play(Walk);
        context.CoroutineController.StartStateCoroutine(AttackRangeDetect());
    }

    public override void ExitState()
    {
        context.Agent.SetDestination(context.Transform.position);
        context.CoroutineController.StopAllCoroutines();
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
        if (context.Agent.velocity != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(context.Agent.velocity.normalized);
            context.Transform.rotation = Quaternion.Slerp(context.Transform.rotation, desiredRotation, Time.deltaTime * 10f);
            WalkFootstep();
        }
        else
        {
            if (Vector3.Distance(context.Transform.position,context.Target.position)<3)
            {
                FaceTo();
            }
            else
            {
                context.Agent.SetDestination(context.Target.position);
            }
        }
    }
    private void WalkFootstep()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            context.AudioSource.PlayWalkFootstep();
            footstepTimer = 1;
        }
    }
    private IEnumerator AttackRangeDetect()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            if (IsPlayerInAttackRange())
            {
                ChangeStateTo(Monster.MonsterState.Attack);
                break;
            }
            yield return wait;
        }
    }
    private bool IsPlayerInAttackRange()
    {
        Collider[] hit = Physics.OverlapSphere(context.AttackDetect.position, attackRange, 1 << 6);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.GetComponent<Player>() != null)
            {
                context.Target = hit[i].gameObject.transform;
                return true;
            }
        }
        return false;
    }
    private void FaceTo()
    {
        float rotateSpeed = 10;
        Vector3 relativePos = context.Target.position - context.Transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        context.Transform.rotation = Quaternion.Lerp(context.Transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
