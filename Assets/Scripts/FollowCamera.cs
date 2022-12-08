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

    public bool lookAtMode = false; //처다보기 모드를 할것인지 확인하는 변수
    
    public bool finish=false; // 경기가 끝났는지 확인하는 변수

    private void FixedUpdate()
    {
        //경기가 끝나지 않았으면
        if (!finish)
        {
            //lookAtMode가 false이면 따라다닌다.
            if (target != null && !lookAtMode)
            {
                //transform.position = target.position + offset;
                transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed * Time.fixedDeltaTime);

            }

            //lookAtMode가 true이면 서서히처다본다.
            if (target != null && lookAtMode)
            {
                Vector3 lookDir = target.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), cameraSpeed * Time.fixedDeltaTime);

            }
        }
    }
}
