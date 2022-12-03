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
    public float cameraSpeed = 3.0f;

    public bool lookAtMode = false; 
    
    public bool finish=false;

    private void FixedUpdate()
    {
        if (!finish)
        {

            if (target != null && !lookAtMode)
            {
                //transform.position = target.position + offset;
                transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed * Time.fixedDeltaTime);

            }

            if (target != null && lookAtMode)
            {
                Vector3 lookDir = target.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), cameraSpeed * Time.fixedDeltaTime);

            }
        }
    }
}
