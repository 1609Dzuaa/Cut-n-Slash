﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using static GameEnums;

public struct ArrowInfor
{
    public ArrowInfor(string id, Vector3 pos, bool isRight)
    {
        ID = id;
        Position = pos;
        IsDirectionRight = isRight;
    }

    public string ID;
    public Vector3 Position;
    public bool IsDirectionRight;
}

public class ArrowController : GameObjectController
{
    [SerializeField, Range(0f, 15f)] float _speed;
    [SerializeField] float _existTime;
    [SerializeField, Tooltip("Với object thẳng đứng thì" +
        " isFr sẽ hướng lên và ngược lại")] bool _isHorizontal;

    float _entryTime;
    string _arrowID;

    public string ID { get => _arrowID; }

    //Với Arrow thì sẽ setup ở Awake thay vì Start (vì arrow có sd OnEnable) 
    protected override void Awake()
    {
        base.Awake();
        SetupProperties();
    }

    protected override void Start() { }

    protected override void SetupProperties()
    {
        base.SetupProperties();
        _arrowID = Guid.NewGuid().ToString();
        //Debug.Log("Setup");
    }

    protected override void OnEnable()
    {
        _entryTime = Time.time;
        EventsManager.Instance.SubcribeToAnEvent(EEvents.ArrowOnReceiveInfor, ReceiveInfor);
    }

    protected override void OnDisable()
    {
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.ArrowOnReceiveInfor, ReceiveInfor);
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
            //Dealing Dmg
            //Lấy vị trí va chạm
            ContactPoint2D contacts = collision.GetContact(0);
            SpawnBloodVfx(contacts.point);
            gameObject.SetActive(false);
        }
    }

    private void SpawnBloodVfx(Vector3 pos)
    {
        GameObject bloodVfx = PoolManager.Instance.GetObjectInPool(EPoolable.BloodVfx);
        bloodVfx.SetActive(true);
        bloodVfx.transform.position = pos;
    }

    private void FixedUpdate()
    {
        Vector2 vectorSpeed = new(0f, 0f);

        if (!_isHorizontal)
        {
            vectorSpeed = new(_speed, 0f);
            vectorSpeed = (_isFacingRight) ? vectorSpeed : vectorSpeed * Vector2.left;
        }
        else
        {
            vectorSpeed = new(0f, _speed);
            vectorSpeed = (_isFacingRight) ? vectorSpeed : vectorSpeed * Vector2.down;
        }

        _rb.velocity = vectorSpeed;
    }

    private void ReceiveInfor(object obj)
    {
        ArrowInfor info = (ArrowInfor)obj;
        if (_arrowID != info.ID) return;

        transform.position = info.Position;
        if (_isFacingRight != info.IsDirectionRight) FlippingSprite();
        //Debug.Log("Shooted");
    }
}
