using Unity.Entities;
using Unity.Collections;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private EntityManager _entityManager;

    private void Start()
    {
        if (World.DefaultGameObjectInjectionWorld != null)
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_entityManager == default) return;

        var pickup = other.GetComponent<PickupAuthoring>();
        if (pickup != null)
        {
            ModifyHealth(pickup.healAmount);
            Debug.Log($"<color=green>Аптечка подобрана!</color> +{pickup.healAmount}");
            Destroy(other.gameObject);
            return;
        }

        if (other.GetComponent<PerkBounceAuthoring>() != null)
        {
            SetBulletsBouncy(true);
            Debug.Log("<color=yellow>БОНУС: Теперь пули рикошетят!</color>");
            Destroy(other.gameObject);
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_entityManager == default) return;

        var entityObj = other.GetComponent<GameObjectEntity>();
        if (entityObj != null)
        {
            Entity zoneEntity = entityObj.Entity;
            if (_entityManager.HasComponent<DamageZoneData>(zoneEntity))
            {
                float damage = _entityManager.GetComponentData<DamageZoneData>(zoneEntity).DamagePerSecond;
                ApplyDamage(damage * Time.deltaTime);
            }
        }
        else
        {
            var damageZone = other.GetComponent<DamageZoneAuthoring>();
            if (damageZone != null)
            {
                ApplyDamage(damageZone.damageAmount * Time.deltaTime);
            }
        }
    }

    private void ModifyHealth(float amount)
    {
        EntityQuery query = _entityManager.CreateEntityQuery(typeof(Health), typeof(PlayerTag));
        using (var entities = query.ToEntityArray(Allocator.TempJob))
        {
            if (entities.Length > 0)
            {
                var h = _entityManager.GetComponentData<Health>(entities[0]);
                h.Current = Mathf.Min(h.Max, h.Current + amount);
                _entityManager.SetComponentData(entities[0], h);
            }
        }
    }

    private void ApplyDamage(float amount)
    {
        EntityQuery query = _entityManager.CreateEntityQuery(typeof(Health), typeof(PlayerTag));
        using (var entities = query.ToEntityArray(Allocator.TempJob))
        {
            if (entities.Length > 0)
            {
                var h = _entityManager.GetComponentData<Health>(entities[0]);
                h.Current = Mathf.Max(0, h.Current - amount);
                _entityManager.SetComponentData(entities[0], h);

                if (Time.frameCount % 60 == 0)
                    Debug.Log($"<color=red>УРОН!</color> HP: {h.Current:F1}");
            }
        }
    }

    private void SetBulletsBouncy(bool status)
    {
        BulletCollisionHandler.GlobalBounceEnabled = status;

        EntityQuery query = _entityManager.CreateEntityQuery(typeof(BulletData));
        using (var entities = query.ToEntityArray(Allocator.TempJob))
        {
            foreach (var e in entities)
            {
                _entityManager.SetComponentData(e, new BulletData { IsBouncy = status });
            }
        }
    }
}