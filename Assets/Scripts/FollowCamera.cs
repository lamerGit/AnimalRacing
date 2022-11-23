using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정대상을 따라가는 스크립트
/// </summary>
public class FollowCamera : MonoBehaviour
{

    public Transform target; //따라다닐 타겟

    public Vector3 offset; // 대상과의 거리

    
    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
           

        }
    }
}
