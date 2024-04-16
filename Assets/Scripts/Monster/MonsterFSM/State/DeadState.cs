using UnityEngine;

public class DeadState : MonsterState
{
    const string Dead = "root|Anim_monster_scavenger_Death";
    public DeadState(MonsterFSMContext _context) : base(_context)
    {
    }
    public override void EnterState()
    {
        context.Agent.isStopped = true;
        context.Agent.velocity = Vector3.zero;
        context.Agent.SetDestination(context.Transform.position);
        context.AudioSource.PlayDeathSound();
        context.Animator.Play(Dead);
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
        //Debug.Log(context.Transform.position);
    }
}
