using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : AnimalAI
{
    //양의 스크립트 

    float skillCoolTime = 15.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 15.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float panicSpeed = 3.0f; // 패닉달리기 시전중에 추가스피드
    float panicTime = 5.0f;  // 패닉달리기 시전 시간
    float panicTimeReset = 5.0f; // 패닉달리기가 발동되고 시간을 초기화해줄 변수

    bool panicCheck = false; // 패닉달리기중인지 확인할 변수

    float sightRange = 10.0f; // 주변 탐색범위 변수
    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                PanicRunReset();
            }


        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!panicCheck)
        {
            //패닉달리기 상태가 아닐때는 쿨타임감소
            skillCoolTime -= Time.fixedDeltaTime;
        }
        else
        {
            //패닉달리기 상태일때 패닉시간감소
            panicTime -= Time.fixedDeltaTime;
            rigid.AddTorque(transform.up * Mathf.Cos(panicTime*4.0f)*avoidSpeed);

        }

        if (panicTime < 0)
        {
            //패닉달리기 상태가 끝나면 상태 리셋
            PanicRunReset();
        }

        // 쿨타임이 되고 상태이상이 아니며 발동
        if (skillCoolTime < 0 && !StateAttack)
        {

            PanicRun();
        }



    }

    protected override void Sensor()
    {
        //패닉달리기중에는 센서비활성화
        if (!panicCheck)
        {
            base.Sensor();
        }
    }

    /// <summary>
    /// 패닉달리기 살짝점프한다음에 스피드가 상승하고 회피를 안하고 우왕좌왕한다.
    /// </summary>
    void PanicRun()
    {
        skillCoolTime = skillCoolTimeReset;
        if (AnimalCheck())
        {
            panicCheck = true;
            rigid.AddForce(transform.up * 10.0f, ForceMode.VelocityChange);
            aiSpeed += panicSpeed;
        }
    }

    /// <summary>
    /// 패닉달리기가 끝나고 리셋
    /// </summary>
    void PanicRunReset()
    {
        panicTime = panicTimeReset;
        if (panicCheck) // 패닉달리기가 true일때만 속도를 감소시켜야한다.
        {               // 이렇게 하지않으면 상태이상걸렸을때 속도가 감소한다.
            aiSpeed -= panicSpeed;
        }

        panicCheck = false;
    }

    /// <summary>
    /// 주변에 다른동물들이 있는지 확인
    /// </summary>
    /// <returns></returns>
    bool AnimalCheck()
    {
        bool result = false;
        //나를 포함에 범위안에 모든 동물을 찾는다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        if (colliders.Length > 0)
        {
            int count = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                count++;

            }
            if(count>3)
            {
                result = true;
            }
        }
        

        return result;
    }
}
