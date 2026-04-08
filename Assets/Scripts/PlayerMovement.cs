using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private IGameConfig _config;

    [Inject]
    public void Construct(IGameConfig config)
    {
        _config = config;
    }

    void Start()
    {
        Debug.Log($"Скорость игрока установлена: {_config.MoveSpeed}");
    }
}
