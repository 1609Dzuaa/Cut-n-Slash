using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class SwordController : MonoBehaviour
{
    [Header("Damage Related")]
    [SerializeField] float _dmgDealt;

    bool _hasDealtDmg;

    private void OnEnable()
    {
        _hasDealtDmg = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(GameConstants.ENEMIES_TAG) && !_hasDealtDmg)
        {
            _hasDealtDmg = true;
            EventsManager.Instance.NotifyObservers(EEvents.EnemiesOnReceiveDamage, _dmgDealt);
        }
    }
}
