using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private UserInputSystem inputSystem;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator _animator;

    private Rigidbody rb;
    private IAbility shootAbility;
    private IAbility dashAbility;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

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
        float magnitude = moveInput.magnitude;

        if (magnitude > 0.1f)
        {
            if (_animator != null)
            {
                _animator.SetFloat("Speed", magnitude);
            }

            rb.velocity = moveDir * moveSpeed;

            transform.forward = moveDir;
        }
        else
        {
            if (_animator != null)
            {
                _animator.SetFloat("Speed", 0f);
            }

            rb.velocity = Vector3.zero;
        }
    }

    private void HandleAbilities()
    {
        if (inputSystem.CurrentInput.IsShootPressed) shootAbility?.Execute();
        if (inputSystem.CurrentInput.IsDashPressed) dashAbility?.Execute();

        inputSystem.ResetTriggers();
    }

    public void TakeDamage()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("TakeDamage");
        }
    }

    public void Die()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }

        this.enabled = false;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
