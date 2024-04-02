using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    protected Animator _anim;

    public Animator GetAnim { get => _anim; set => _anim = value; }

    protected virtual void Awake()
    {
        GetReferenceComponents();
    }

    protected virtual void GetReferenceComponents()
    {
        _anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    // Start is called before the first frame update
    protected virtual void Start() { SetupProperties(); }

    protected virtual void SetupProperties() { }

    // Update is called once per frame
    protected virtual void Update() { }
}
