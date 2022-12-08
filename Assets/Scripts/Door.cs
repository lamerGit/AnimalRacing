using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //게이트문을 여는 스크립트

    public Animator[] doorAnimators; 


    public void DoorOpen()
    {
        for (int i = 0; i < doorAnimators.Length; i++)
        {
            doorAnimators[i].SetTrigger("Open");
        }
    }
}
