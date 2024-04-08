using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/EnemiesStats")]
public class EnemiesSO : ScriptableObject
{
    [Header("Layers")]
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] LayerMask _gwLayer;

    [Header("Check Distance")]
    [SerializeField] float _playerCheckDistance;
    [SerializeField] float _gwCheckDistance;

    [Header("Speed")]
    [SerializeField] float _patrolSpeed;

    [Header("Force")]
    [SerializeField] Vector2 _knockForce;

    [Header("Time")]
    [SerializeField] float _attackDelay;
    [SerializeField] float _restTime;
    [SerializeField] float _patrolTime;
    [SerializeField] float _delayFlipIfPlayerBackward;

    public LayerMask PlayerLayer { get { return _playerLayer; } }

    public LayerMask GWLayer { get { return _gwLayer; } }

    public float PlayerCheckDistance { get { return _playerCheckDistance; } }

    public float GWCheckDistance { get { return _gwCheckDistance; } }

    public float PatrolSpeed { get { return _patrolSpeed; } }

    public Vector2 KnockForce { get { return _knockForce; } }

    public float AttackDelay { get { return _attackDelay; } }

    public float RestTime { get { return _restTime;} }

    public float PatrolTime { get { return _patrolTime; } }

    public float DelayFlipIfPlayerBackward { get { return _delayFlipIfPlayerBackward; } }
}
