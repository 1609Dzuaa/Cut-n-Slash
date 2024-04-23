using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : GameObjectController
{
    [SerializeField] float _onTime;

    bool _hasTriggered;
    float _entryTime;

    protected override void Update()
    {
        if (Time.time - _entryTime >= _onTime)
        {
            _hasTriggered = false;
            _anim.SetBool(GameConstants.TRAP_ANIM_PARA, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(GameConstants.PLAYER_TAG) && !_hasTriggered)
        {
            _hasTriggered = true;
            _entryTime = Time.time;
            _anim.SetBool(GameConstants.TRAP_ANIM_PARA, true);
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstants.PLAYER_TAG) && !_hasTriggered)
        {
            _hasTriggered = true;
            _entryTime = Time.time;
            _anim.SetBool(GameConstants.TRAP_ANIM_PARA, true);
        }
    }

    private void ChangeTrapTag()
    {
        gameObject.tag = GameConstants.TRAP_TAG;
    }

    private void ChangeDefaultTag()
    {
        gameObject.tag = GameConstants.UNTAGGED_TAG;

    }
}
