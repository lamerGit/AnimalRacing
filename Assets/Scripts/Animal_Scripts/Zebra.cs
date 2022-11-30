using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : AnimalAI
{
    //얼룩말의 스크립트 

    float skillCoolTime = 18.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 18.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float runSpeed = 20.0f; // 전력질주 시전중에 추가스피드
    float runTime = 4.0f;  // 전력질주 시전 시간
    float runTimeReset = 4.0f; // 전력질주가 발동되고 시간을 초기화해줄 변수

    bool runCheck = false; // 전력질주중인지 확인할 변수

    GameObject cfx_Run;

    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                RunReset();
            }


        }
    }
    protected override void Start()
    {
        base.Start();
        cfx_Run = transform.Find("CFXR3_Run").gameObject;
        cfx_Run.SetActive(false);


        aiSpeed = 58.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!runCheck)
        {
            //전력질주 상태가 아닐때는 쿨타임감소
            skillCoolTime -= Time.fixedDeltaTime;
        }
        else
        {
            //전력질주 상태일때 패닉시간감소
            runTime -= Time.fixedDeltaTime;

        }

        if (runTime < 0)
        {
            //전력질주 상태가 끝나면 상태 리셋
            RunReset();
        }

        // 쿨타임이 되고 상태이상이 아니며 발동
        if (skillCoolTime < 0 && !StateAttack)
        {

            Run();
        }



    }



    /// <summary>
    /// 전력질주 스피드가 상승한다.
    /// </summary>
    void Run()
    {
        skillCoolTime = skillCoolTimeReset;
        runCheck = true;
        cfx_Run.SetActive(runCheck);
        aiSpeed += runSpeed;


    }

    /// <summary>
    /// 전력질주가 끝나고 리셋
    /// </summary>
    void RunReset()
    {
        runTime = runTimeReset;
        if (runCheck) // 전력질주가 true일때만 속도를 감소시켜야한다.
        {               // 이렇게 하지않으면 상태이상걸렸을때 속도가 감소한다.
            aiSpeed -= runSpeed;
        }

        runCheck = false;
        cfx_Run.SetActive(runCheck);
    }

    
   
}
