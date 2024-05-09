using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameEnums;

public struct PopupDMGInfor
{
    public PopupDMGInfor(string id, Vector3 pos, float damage)
    {
        ID = id;
        Position = pos;
        Damage = damage;
    }

    public string ID;
    public Vector3 Position;
    public float Damage;
}

public class PopupDMG : MonoBehaviour
{
    [SerializeField] TextMeshPro _pUpText;
    [SerializeField] float _pUpSpeed;
    [SerializeField] float _existTime;
    [SerializeField] float _alphaDecreaseOverTime;

    float _entryTime;
    string _pUpTextID;

    public string ID { get => _pUpTextID; }

    private void Awake()
    {
        _pUpTextID = Guid.NewGuid().ToString(); 
    }

    private void OnEnable()
    {
        _entryTime = Time.time;
        _pUpText.alpha = 1.0f;
        EventsManager.Instance.SubcribeToAnEvent(EEvents.PopupTextOnReceiveInfor, ReceiveInfor);
    }

    private void OnDisable()
    {
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.PopupTextOnReceiveInfor, ReceiveInfor);
    }

    private void ReceiveInfor(object obj)
    {
        PopupDMGInfor info = (PopupDMGInfor)obj;
        if (_pUpTextID != info.ID) return;

        transform.position = info.Position;
        _pUpText.text = info.Damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _entryTime < _existTime)
        {
            transform.position += new Vector3(0f, _pUpSpeed, 0f) * Time.deltaTime;
            _pUpText.alpha -= _alphaDecreaseOverTime;
        }
        else
            gameObject.SetActive(false);
    }
}
