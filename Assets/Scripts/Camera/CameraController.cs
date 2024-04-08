using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _targetToFollow;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(_targetToFollow.position.x, _targetToFollow.position.y, -10);
    }
}
