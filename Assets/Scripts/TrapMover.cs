using UnityEngine;
using DG.Tweening; 

public class TrapMover : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float _dropHeight = 5f;  
    [SerializeField] private float _dropDuration = 0.2f; 
    [SerializeField] private float _riseDuration = 2f; 
    [SerializeField] private float _waitAtBottom = 1f;  

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        StartTrapCycle();
    }

    private void StartTrapCycle()
    {
        Sequence trapSequence = DOTween.Sequence();

        trapSequence.Append(transform.DOMoveY(_startPosition.y - _dropHeight, _dropDuration).SetEase(Ease.InExpo))
                    .AppendInterval(_waitAtBottom) 
                    .Append(transform.DOMoveY(_startPosition.y, _riseDuration).SetEase(Ease.OutSine)) 
                    .SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (player.enabled)
            {
                player.Die();
                Debug.Log("Ловушка сработала!");
            }
        }
    }
}