using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    protected Animator _anim;
    protected Rigidbody2D _rb;
    [SerializeField, Tooltip("Tick vào để chỉ hướng mặc định của Sprite")] 
    protected bool _isFacingRight;

    public Animator GetAnim { get => _anim; set => _anim = value; }

    public Rigidbody2D GetRigidbody2D { get => _rb; set => _rb = value; }


    public bool IsFacingRight { get => _isFacingRight; }

    protected virtual void Awake()
    {
        GetReferenceComponents();
    }

    protected virtual void GetReferenceComponents()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    // Start is called before the first frame update
    protected virtual void Start() { SetupProperties(); }

    protected virtual void SetupProperties() 
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.y) >= 180f && _isFacingRight)
            _isFacingRight = false;
        else if(Mathf.Abs(transform.rotation.eulerAngles.y) >= 180f && !_isFacingRight)
            _isFacingRight = true;
    }

    // Update is called once per frame
    protected virtual void Update() { }

    public void FlippingSprite()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}
