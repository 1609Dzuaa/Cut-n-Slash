using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTeleportState : EnemiesBaseState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)GameEnums.EArcherState.Teleport);
        _archerSM = (ArcherStateManager)_enemiesSM;
        HandleTeleport();
        Debug.Log("Archer Tele");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleTeleport()
    {
        float teleDist = _archerSM.TeleportDist;
        float playerPosX = _archerSM.PlayerRef.position.x;
        float playerPosY = _archerSM.PlayerRef.position.y;
        float playerPosZ = _archerSM.PlayerRef.position.z;

        if (_archerSM.IsFacingRight)
            _archerSM.transform.position = new Vector3(playerPosX + teleDist, playerPosY, playerPosZ);
        else
            _archerSM.transform.position = new Vector3(playerPosX - teleDist, playerPosY, playerPosZ);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}