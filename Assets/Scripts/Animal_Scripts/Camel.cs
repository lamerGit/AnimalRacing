using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : AnimalAI
{
    //낙타의 스크립트

    

    float furyRunSpeed = 15.0f; // 격노질주 스피드
    WaitForSeconds furyRunSecond = new WaitForSeconds(4.0f); // 격노질주가 끝나는 시간

    GameObject cfx_FuryRun;
    Transform[] cfx_FuryRun_Child;
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
                for(int i=0; i<cfx_FuryRun_Child.Length; i++)
                {
                    cfx_FuryRun_Child[i].gameObject.SetActive(true);
                }

                StartCoroutine(furyRunReset());
            }



        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_FuryRun = transform.Find("CFX_FuryRun").gameObject;
        cfx_FuryRun_Child=cfx_FuryRun.GetComponentsInChildren<Transform>();
        cfx_FuryRun.SetActive(false);
        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    IEnumerator furyRunReset()
    {
        yield return furyRunSecond;
        cfx_FuryRun.SetActive(false);
        aiSpeed -= furyRunSpeed;
    }

    
}
