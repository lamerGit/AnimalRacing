using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : AnimalAI
{
    //낙타의 스크립트

    

    float furyRunSpeed = 15.0f; // 격노질주 스피드
    WaitForSeconds furyRunSecond = new WaitForSeconds(4.0f); // 격노질주가 끝나는 시간

    GameObject cfx_FuryRun;
    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (!base.StateAttack)
            {
                aiSpeed += furyRunSpeed;
                cfx_FuryRun.SetActive(true);
                StartCoroutine(furyRunReset());
            }



        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_FuryRun = transform.Find("CFX_FuryRun").gameObject;
        cfx_FuryRun.SetActive(false);
    }

    IEnumerator furyRunReset()
    {
        yield return furyRunSecond;
        cfx_FuryRun.SetActive(false);
        aiSpeed -= furyRunSpeed;
    }

    
}
