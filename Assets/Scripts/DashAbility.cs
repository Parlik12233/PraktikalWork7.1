using UnityEngine;

public class DashAbility : AbilityBase
{
    [SerializeField] private float dashForce = 15f;
    private Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    protected override void PerformAbility()
    {
        Vector3 dashDirection = transform.forward;
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }
}
