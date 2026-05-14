using System;

[Serializable]
public class PlayerConfig : IGameConfig
{
    public float MaxHealth;
    public float BulletSpeed;

    public float MoveSpeed => 7f;
    int IGameConfig.MaxHealth => (int)MaxHealth;
}
