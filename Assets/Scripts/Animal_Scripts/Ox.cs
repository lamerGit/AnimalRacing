using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ox : AnimalAI
{
    //황소의 스크립트
    
    bool madSpin=false; //미친회전이 발동중인지 확인하는 변수
    float skillCoolTime = 8.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 10.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float madSpinSpeed = 5.0f; //미친회전 상태일때 상승하는 스피드
    float madSpinTime = 5.0f; // 미친회전 시전되는 시간
    float madSpinTimeReset = 5.0f; // 미친회전이 발동되고 시간을 초기화해줄 변수

    float madPower = 20.0f; // 미친회전에 당한 동물들이 감소될 스피드

    /// <summary>
    /// 상태이상 체크용 프로퍼티 상태이상이 걸리면 미친회전이 취소된다.
    /// </summary>
    protected override bool StateAttack { get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                MadnessSpinReset();
            }


        }
    }
     
    protected override void Start()
    {
        base.Start();
        aiSpeed = 59.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //미친회전 상태가 아닐때만 쿨타임감수
        if (!madSpin)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }else
        {
            //미친회전 상태일때는 시전시간 감소
            madSpinTime -= Time.fixedDeltaTime;
        }

        if(madSpinTime<0)
        {
            //시전 시간이 전부 감소하면 상태리셋
            MadnessSpinReset();
        }

        if (skillCoolTime < 0 && !StateAttack)
        {
           //상태이상이 아니고 스킬 쿨타임이 0보다 작아지면 발동
            MadnessSpin();
        }
    }

    /// <summary>
    /// 미친회전상태일때는 회피하지 않고 쭉달려가기위해 Sensor을 비활성화 한다.
    /// </summary>
    protected override void Sensor()
    {
        if (!madSpin)
        {
            base.Sensor();
        }
    }

    /// <summary>
    /// 미친회전이 끝나고 상태를 리셋하는 함수
    /// </summary>
    private void MadnessSpinReset()
    {
        madSpinTime = madSpinTimeReset;
        if(madSpin)
        {
            aiSpeed -= madSpinSpeed;
        }

        madSpin = false;
        
        animator.SetBool("MadnessSpin", madSpin);
    }

    /// <summary>
    /// 미친회전을 시전하는 함수
    /// </summary>
    void MadnessSpin()
    {
        skillCoolTime = skillCoolTimeReset;
        madSpin =true;
        aiSpeed += madSpinSpeed;
        animator.SetBool("MadnessSpin", madSpin);
    }

    /// <summary>
    /// 미친회전상태일때 다른 동물들과 접촉하면 상태이상 스핀을 걸게된다.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if(madSpin)
        {
            if(collision.gameObject.CompareTag("Animal"))
            {
                collision.gameObject.GetComponent<IHit>().TakeHit(madPower,HitType.Spin);
            }
        }
    }
}
