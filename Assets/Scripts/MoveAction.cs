using UnityEngine;

public class MoveAction : EnemyAction
{
    public MoveAction(EnemyBrain brain) : base(brain) { }

    public override float GetScore()
    {
        if (brain.Target == null) return 0f;

        float distance = Vector3.Distance(brain.transform.position, brain.Target.position);
        return distance > brain.AttackRange ? 1f : 0f;
    }

    public override void Execute()
    {
        if (brain.Agent.isStopped) brain.Agent.isStopped = false;
        brain.Agent.SetDestination(brain.Target.position);
    }
}
