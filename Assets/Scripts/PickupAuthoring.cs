using Unity.Entities;
using UnityEngine;

public class PickupAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float healAmount;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new HealthPickup { Amount = healAmount });
    }
}