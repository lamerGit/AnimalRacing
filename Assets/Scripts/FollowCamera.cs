using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ư������� ���󰡴� ��ũ��Ʈ
/// </summary>
public class FollowCamera : MonoBehaviour
{

    public Transform target; //����ٴ� Ÿ��

    public Vector3 offset; // ������ �Ÿ�

    
    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
           

        }
    }
}
