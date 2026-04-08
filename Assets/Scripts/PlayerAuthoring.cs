using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float maxHealth = 100f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new Health { Current = maxHealth, Max = maxHealth });
        dstManager.AddComponentData(entity, new PlayerInputData()); 
        dstManager.AddComponentData(entity, new PlayerTag());
    }
}