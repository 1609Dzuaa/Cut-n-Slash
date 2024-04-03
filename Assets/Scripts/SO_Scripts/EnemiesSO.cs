using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/EnemiesStats")]
public class EnemiesSO : ScriptableObject
{
    [Header("Layers")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _gwLayer;

    [Header("Check Distance")]
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _gwCheckDistance;

    [Header("Speed")]
    [SerializeField] private float _patrolSpeed;

    [Header("Force")]
    [SerializeField] private Vector2 _knockForce;

    [Header("Time")]
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _restTime;
    [SerializeField] private float _patrolTime;

    public LayerMask PlayerLayer { get { return _playerLayer; } }

    public LayerMask GWLayer { get { return _gwLayer; } }

    public float PlayerCheckDistance { get { return _playerCheckDistance; } }

    public float GWCheckDistance { get { return _gwCheckDistance; } }

    public float PatrolSpeed { get { return _patrolSpeed; } }

    public Vector2 KnockForce { get { return _knockForce; } }

    public float AttackDelay { get { return _attackDelay; } }

    public float RestTime { get { return _restTime;} }

    public float PatrolTime { get { return _patrolTime; } }
}
