using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform target; //처다볼 타겟

    public float cameraSpeed = 3.0f;

    private void FixedUpdate()
    {

        if (target != null)
        {
            
            //transform.LookAt(target);
            Vector3 lookDir=target.position-transform.position;

            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookDir),cameraSpeed*Time.fixedDeltaTime);

        }
    }
}
