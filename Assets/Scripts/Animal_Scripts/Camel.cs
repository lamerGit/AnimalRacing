using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : AnimalAI
{
    //낙타의 스크립트

    

    float furyRunSpeed = 20.0f; // 격노질주 스피드
    WaitForSeconds furyRunSecond = new WaitForSeconds(4.0f); // 격노질주가 끝나는 시간


    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (!base.StateAttack)
            {
                aiSpeed += furyRunSpeed;
                StartCoroutine(furyRunReset());
            }



        }
    }

    IEnumerator furyRunReset()
    {
        yield return furyRunSecond;
        aiSpeed -= furyRunSpeed;
    }

    
}
