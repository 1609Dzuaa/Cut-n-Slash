using UnityEngine;

public class SawController : MovingObjectController
{
    [SerializeField] float _rotateSpeed;
    [SerializeField] bool _isBossGate;

    protected override void Start()
    {
        if (_isBossGate)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (_isBossGate)
        {
            //Debug.Log("Unsub");
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_isBossGate)
            base.Update();
        transform.Rotate(0f, 0f, 360f * _rotateSpeed * Time.deltaTime);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstants.SHIELD_TAG))
            gameObject.SetActive(false);
    }*/

    /*/// <summary>
    /// Hàm dưới để active gate khi Player enter Boss Room
    /// </summary>*/

    /*private void ActiveGate(object obj)
    {
        Debug.Log("activated");
        GameObject activeVfx = Pool.Instance.GetObjectInPool(GameEnums.EPoolable.RedExplode);
        activeVfx.SetActive(true);
        activeVfx.transform.position = transform.position;
        gameObject.SetActive(true);
    }*/
}