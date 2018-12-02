using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    private Camera _camera;
    private Transform _target;
    private bool _followActive;

    public void Initialize()
    {
        _camera = GetComponent<Camera>();
        _followActive = false;
    }

    public void FollowTarget(Transform target, bool autoFollow = true)
    {
        _target = target;
        _followActive = autoFollow;
    }

    void LateUpdate()
    {
        if (_followActive == false || _target == null) return;

        _camera.transform.position = new Vector3(_target.position.x, _target.position.y, -10);
    }
}
