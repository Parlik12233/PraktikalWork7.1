using Unity.Entities;
using Unity.Mathematics;

public struct PlayerInputData : IComponentData
{
    public float2 Move;
    public bool ShootRequested;
    public bool HasBouncePerk;
}

public struct Health : IComponentData
{
    public float Current;
    public float Max;
}

public struct PlayerTag : IComponentData { }

public struct HealthPickup : IComponentData
{
    public float Amount;
}

public struct BulletData : IComponentData
{
    public bool IsBouncy;
}

public struct WallTag : IComponentData { }

public struct DamageZoneData : IComponentData
{
    public float DamagePerSecond;
}