using Unity.Entities;
using UnityEngine;

public class DamageZoneAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float damageAmount = 10f; 

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new DamageZoneData { DamagePerSecond = damageAmount });
    }
}
