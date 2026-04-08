using UnityEngine;

public abstract class AbilityBase : MonoBehaviour, IAbility
{
    [SerializeField] protected float cooldown = 0.5f;
    protected float lastExecuteTime = -100f;

    public virtual void Execute()
    {
        if (Time.time < lastExecuteTime + cooldown) return;

        PerformAbility();
        lastExecuteTime = Time.time;
    }

    protected abstract void PerformAbility();
}
