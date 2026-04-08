using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public Transform playerTransform;
    public EnemyBrain[] enemies;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.Initialize(playerTransform);
        }
    }
}
