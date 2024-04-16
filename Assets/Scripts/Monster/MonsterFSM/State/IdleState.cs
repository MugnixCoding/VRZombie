using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : MonsterState
{
    const string Idle = "root|Anim_monster_scavenger_Idle1";
    public IdleState(MonsterFSMContext _context) : base(_context)
    {
    }
    public override void EnterState()
    {
        GameObject player = GameObject.Find("Player");
        context.AudioSource.PlayLurkSound();
        context.Animator.Play(Idle);
        if (player == null)
        {
            context.Target =null;
            context.CoroutineController.StartCoroutine(SearchPlayer());
        }
        else
        {
            context.Target = player.transform;
            ChangeStateTo(Monster.MonsterState.Move);
        }
    }

    public override void ExitState()
    {
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
    }
    private IEnumerator SearchPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                context.Target = player.transform;
                ChangeStateTo(Monster.MonsterState.Move);
            }
        }
    }
}
