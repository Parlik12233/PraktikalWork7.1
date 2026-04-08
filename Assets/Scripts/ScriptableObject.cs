using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class ScriptableGameConfig : ScriptableObject, IGameConfig
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _maxHealth = 100;

    public float MoveSpeed => _moveSpeed;
    public int MaxHealth => _maxHealth;
}

public class RemoteConfigLoader : IGameConfig
{
    private readonly ScriptableGameConfig _config;

    public RemoteConfigLoader(ScriptableGameConfig config)
    {
        _config = config;
    }

    public float MoveSpeed => _config.MoveSpeed;
    public int MaxHealth => _config.MaxHealth;
}
