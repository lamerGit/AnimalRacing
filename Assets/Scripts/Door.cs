using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator[] doorAnimators; 



    private void Start()
    {
        
        for(int i=0; i < doorAnimators.Length; i++)
        {
            doorAnimators[i].SetTrigger("Open");
        }

    }


}
