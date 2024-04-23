using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class SwordController : MonoBehaviour
{
    [Header("Damage Related")]
    [SerializeField] float _dmgDeal;

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
            string enemyID = collision.gameObject.GetComponent<EnemiesStateManager>().ID;

            DamageReceiveInfor info = new(enemyID, _dmgDeal);
            EventsManager.Instance.NotifyObservers(EEvents.EnemiesOnReceiveDamage, info);
        }
    }
}
