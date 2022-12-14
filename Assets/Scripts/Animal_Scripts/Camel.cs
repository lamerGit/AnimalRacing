using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : AnimalAI
{
    //낙타의 스크립트

    

    float furyRunSpeed = 17.0f; // 격노질주 스피드
    bool furyRunCheck=false;
    //WaitForSeconds furyRunSecond = new WaitForSeconds(4.0f); // 격노질주가 끝나는 시간
    float furyTime = 4.0f; // 격노질주 시전시간
    float furyTimeReset = 4.0f; // 격노질주 시전시간 초기화 변수

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
                if (RaceStarted)
                {
                    FuryRun();
                }


                //StartCoroutine(furyRunReset());
            }
            else
            {
                FuryRunReset();
            }



        }
    }

    private void FuryRun()
    {
        aiSpeed += furyRunSpeed+0.5f;
        furyRunCheck = true;
        animalAudio.Play();
        cfx_FuryRun.SetActive(furyRunCheck);
        for (int i = 0; i < cfx_FuryRun_Child.Length; i++)
        {
            cfx_FuryRun_Child[i].gameObject.SetActive(furyRunCheck);
        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_FuryRun = transform.Find("CFX_FuryRun").gameObject;
        cfx_FuryRun_Child=cfx_FuryRun.GetComponentsInChildren<Transform>();
        cfx_FuryRun.SetActive(false);
        aiSpeed = Random.Range(58.5f, 60.5f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (RaceStarted)
        {
            if (furyRunCheck) //격노질주 상태이면 격노질주 시간감소
            {
                furyTime -= Time.fixedDeltaTime;

            }

            if (furyTime < 0)
            {
                //분노질주 시간이 끝나면 상태리셋
                FuryRunReset();
            }
        }

    }


    void FuryRunReset()
    {
        cfx_FuryRun.SetActive(false);
        furyTime = furyTimeReset;
        if (furyRunCheck)
        {
            aiSpeed -= furyRunSpeed;
        }
        furyRunCheck=false;

    }

    //IEnumerator furyRunReset()
    //{
    //    yield return furyRunSecond;
    //    cfx_FuryRun.SetActive(false);
    //    aiSpeed -= furyRunSpeed;
    //}

}
