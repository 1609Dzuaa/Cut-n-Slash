using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEyeStateManager : EnemiesStateManager
{
    private void ModifyGravity()
    {
        _rb.gravityScale = 1.0f;
    }
}
