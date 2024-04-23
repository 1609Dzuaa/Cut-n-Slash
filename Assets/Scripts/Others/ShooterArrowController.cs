using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class ShooterArrowController : MonoBehaviour
{
    [SerializeField] Transform _shootPos;
    [SerializeField] float _timeEachShoot;

    Animator _anim;
    float _entryTime;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _entryTime = Time.time;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _entryTime >= _timeEachShoot)
        {
            _anim.SetBool(GameConstants.TRAP_ANIM_PARA, true);
            _entryTime = Time.time;
        }
    }

    private void ShootArrow()
    {
        GameObject arrow = PoolManager.Instance.GetObjectInPool(EPoolable.ShooterArrow);
        arrow.SetActive(true);
        arrow.transform.position = _shootPos.position;
    }

    private void TrapOff()
    {
        _anim.SetBool(GameConstants.TRAP_ANIM_PARA, false);
    }
}
