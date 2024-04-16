using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetState : MonsterState
{
    const string Attack = "root|Anim_monster_scavenger_attack1";
    private float actualAttackRange = 0.8f;
    private bool isAttacking;
    public AttackTargetState(MonsterFSMContext _context) : base(_context)
    {
    }
    public override void EnterState()
    {
        context.Agent.SetDestination(context.Transform.position);
        context.Animator.speed = 1.5f;
        isAttacking = false;
    }

    public override void ExitState()
    {
        context.Animator.speed = 1;
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
        Transform target = null;
        if (IsPlayerInAttackRange(out target))
        {
            context.Target = target;
            FaceTo();
            if (!isAttacking)
            {
                context.CoroutineController.StartCoroutine(OnAttack(target.GetComponent<IDamageable>()));
            }
        }
        else
        {
            if (!isAttacking)
            {
                context.Target = null;
                ChangeStateTo(Monster.MonsterState.Idle);
            }
            else
            {
                FaceTo();
            }
            return;
        }
    }
    private bool IsPlayerInAttackRange(out Transform player)
    {
        player = null;
        Collider[] hit = Physics.OverlapSphere(context.AttackDetect.position, actualAttackRange, 1 << 6);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.GetComponent<Player>() != null)
            {
                IDamageable target = hit[i].gameObject.GetComponent<IDamageable>();
                if (target != null)
                {
                    player = hit[i].transform;
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsPlayerInAttackRange()
    {
        Collider[] hit = Physics.OverlapSphere(context.AttackDetect.position, actualAttackRange, 1 << 6);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].gameObject.GetComponent<Player>() != null)
            {
                IDamageable target = hit[i].gameObject.GetComponent<IDamageable>();
                if (target != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private IEnumerator OnAttack(IDamageable target)
    {
        isAttacking = true;
        context.Animator.Play(Attack);
        context.AudioSource.PlayAttackSound();
        yield return new WaitForSeconds(0.8f);
        if (context.Animator.GetCurrentAnimatorStateInfo(0).IsName(Attack))
        {
            if (IsPlayerInAttackRange())
            {
                target.TakeDamage(1,context.Transform);
            }
            yield return new WaitForSeconds(0.7f);
            context.Animator.Play(Attack, -1, 0f);
            isAttacking = false;
        }
    }
    private void FaceTo()
    {
        float rotateSpeed = 10;
        Vector3 relativePos = context.Target.position - context.Transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        context.Transform.rotation = Quaternion.Lerp(context.Transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
