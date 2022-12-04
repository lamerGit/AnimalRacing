using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    public FollowCamera camera5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Animal"))
        {
            camera5.finish = true;
        }
    }
}
