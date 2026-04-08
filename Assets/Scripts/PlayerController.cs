using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private UserInputSystem inputSystem;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private IAbility shootAbility;
    private IAbility dashAbility;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shootAbility = GetComponent<ShootAbility>();
        dashAbility = GetComponent<DashAbility>();
    }

    private void Update()
    {
        HandleAbilities();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = inputSystem.CurrentInput.Move;
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDir.magnitude > 0.1f)
        {
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
            transform.forward = moveDir;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void HandleAbilities()
    {
        if (inputSystem.CurrentInput.IsShootPressed) shootAbility?.Execute();
        if (inputSystem.CurrentInput.IsDashPressed) dashAbility?.Execute();

        inputSystem.ResetTriggers();
    }
}
