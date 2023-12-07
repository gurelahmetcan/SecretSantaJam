using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private Vector3 _offset;
    
    private Vector3 velocity = Vector3.zero;
    

    void Update()
    {
        if (_target != null)
        {
            Vector3 targetPos = _target.position + _offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, _smoothTime);
        }
    }
}
