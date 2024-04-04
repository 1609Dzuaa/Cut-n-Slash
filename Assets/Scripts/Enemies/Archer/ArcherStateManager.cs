using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStateManager : EnemiesStateManager
{
    [Header("Withdrawn Force")]
    [SerializeField] Vector2 _withdrawnForce;

    public Vector2 WithdrawnForce { get => _withdrawnForce; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
