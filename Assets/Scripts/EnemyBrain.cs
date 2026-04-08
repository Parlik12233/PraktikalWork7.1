using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private float _attackRange = 2.5f;
    [SerializeField] private float _attackCooldown = 2.0f;

    private NavMeshAgent _agent;
    private Transform _target;
    private List<EnemyAction> _actions;
    private EnemyAction _currentAction;

    public NavMeshAgent Agent => _agent;
    public Transform Target => _target;
    public float AttackRange => _attackRange;
    public float AttackCooldown => _attackCooldown;

    public void Initialize(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _actions = new List<EnemyAction>
        {
            new MoveAction(this),
            new AttackAction(this)
        };
    }

    private void Update()
    {
        if (_target == null) return;

        ChooseBestAction();
        _currentAction?.Execute();
    }

    private void ChooseBestAction()
    {
        float highestScore = -1f;
        foreach (var action in _actions)
        {
            float score = action.GetScore();
            if (score > highestScore)
            {
                highestScore = score;
                _currentAction = action;
            }
        }
    }
}
