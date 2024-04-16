using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ArcherTeleportState : EnemiesBaseState
{
    ArcherStateManager _archerSM;

    public override void EnterState(CharactersStateManager charactersSM)
    {
        base.EnterState(charactersSM);
        _enemiesSM.GetAnim.SetInteger(GameConstants.STATE_ANIM, (int)EArcherState.Teleport);
        _archerSM = (ArcherStateManager)_enemiesSM;
        HandleTeleport();
        Debug.Log("Archer Tele");
    }

    public override void ExitState()
    {
        SpawnAppearVfx();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleTeleport()
    {
        SpawnDissapearVfx();

        float teleDist = _archerSM.TeleportDist;
        float playerPosX = _archerSM.PlayerRef.position.x;
        float playerPosY = _archerSM.PlayerRef.position.y;
        float playerPosZ = _archerSM.PlayerRef.position.z;

        if (_archerSM.IsFacingRight)
            _archerSM.transform.position = new Vector3(playerPosX + teleDist, playerPosY, playerPosZ);
        else
            _archerSM.transform.position = new Vector3(playerPosX - teleDist, playerPosY, playerPosZ);

        _archerSM.FlippingSprite();
    }

    private void SpawnDissapearVfx()
    {
        GameObject teleVfx = PoolManager.Instance.GetObjectInPool(EPoolable.ArcherTeleportVfx);
        teleVfx.SetActive(true);
        teleVfx.transform.position = _archerSM.transform.position;
    }

    private void SpawnAppearVfx()
    {
        GameObject appearVfx = PoolManager.Instance.GetObjectInPool(EPoolable.ArcherAppearVfx);
        appearVfx.SetActive(true);
        appearVfx.transform.position = _archerSM.transform.position;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}