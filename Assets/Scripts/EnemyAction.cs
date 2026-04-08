using UnityEngine;

public abstract class EnemyAction
{
    protected EnemyBrain brain;

    protected EnemyAction(EnemyBrain brain)
    {
        this.brain = brain;
    }

    public abstract float GetScore();
    public abstract void Execute();
}