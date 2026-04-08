using Unity.Entities;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    public static bool GlobalBounceEnabled = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerAuthoring>() != null) return;

        if (GlobalBounceEnabled)
        {
            Debug.Log("<color=green>РИКОШЕТ: Глобальный перк активен!</color>");
            ReflectBullet(collision);
        }
        else
        {
            Debug.Log("Обычная пуля: Перк не найден. Уничтожаю.");
            Destroy(gameObject);
        }
    }

    private void ReflectBullet(Collision collision)
    {
        if (rb != null && collision.contacts.Length > 0)
        {
            Vector3 normal = collision.contacts[0].normal;
  
            Vector3 reflectDir = Vector3.Reflect(collision.relativeVelocity, normal);
            rb.velocity = -reflectDir; 

            transform.position += normal * 0.2f;
        }
    }
}