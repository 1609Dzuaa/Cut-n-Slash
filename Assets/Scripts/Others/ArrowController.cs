using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ArrowController : GameObjectController
{
    [SerializeField, Range(0f, 15f)] float _speed;
    [SerializeField] float _existTime;

    float _entryTime;
    bool _isRooted;

    protected override void OnEnable()
    {
        _entryTime = Time.time;
        _isRooted = false;
    }

    protected override void Update()
    {
        if (Time.time - _entryTime >= _existTime)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(GameConstants.PLAYER_TAG))
        {
            //Coi lại
            _isRooted = true;
            transform.SetParent(collision.collider.transform);
        }
    }

    private void FixedUpdate()
    {
        if (_isRooted) return;

        Vector2 vectorSpeed = new(_speed, 0f);
        _rb.velocity = (IsFacingRight) ? vectorSpeed : vectorSpeed * Vector2.left; 
    }
}
