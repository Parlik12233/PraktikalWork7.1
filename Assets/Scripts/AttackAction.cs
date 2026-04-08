using UnityEngine;

public class AttackAction : EnemyAction
{
    private float _lastAttackTime;

    public AttackAction(EnemyBrain brain) : base(brain) { }

    public override float GetScore()
    {
        if (brain.Target == null) return 0f;

        float distance = Vector3.Distance(brain.transform.position, brain.Target.position);
        return distance <= brain.AttackRange ? 1.5f : 0f;
    }

    public override void Execute()
    {
        brain.Agent.isStopped = true;

        if (Time.time > _lastAttackTime + brain.AttackCooldown)
        {
            Debug.Log("<color=red>ООП Атака:</color> Нанесен урон цели!");
            _lastAttackTime = Time.time;
        }
    }
}
