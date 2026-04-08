using UnityEngine;

public class UserInputSystem : MonoBehaviour
{
    private PlayerControls controls;
    public InputData CurrentInput { get; private set; } = new InputData();

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Dash.performed += _ => CurrentInput.IsDashPressed = true;
        controls.Player.Shoot.performed += _ => CurrentInput.IsShootPressed = true;
    }

    private void Update()
    {
        CurrentInput.Move = controls.Player.Move.ReadValue<Vector2>();
    }

    public void ResetTriggers()
    {
        CurrentInput.IsDashPressed = false;
        CurrentInput.IsShootPressed = false;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
