using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    //동물이 결승점을 통과했을때 사용되는 스크립트

    public FollowCamera camera5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Animal"))
        {
            camera5.finish = true;

            
        }
    }
}
