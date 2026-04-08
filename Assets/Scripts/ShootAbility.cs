using UnityEngine;

public class ShootAbility : AbilityBase
{
    [Header("Настройки снаряда")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletLifeTime = 3f; 

    protected override void PerformAbility()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifeTime);
    }
}
