using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcticFox : AnimalAI
{
    //북극여우의 스크립트
     
    float jumpPower = 20.0f; // 얼마나 높이 점프할지 변수
    bool rolling = false; // 점프상태인지 확인하는 변수
    float skillCoolTime = 10.0f; // 쿨타임 초기 시간
    float skillCoolTimeReset = 10.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    /// <summary>
    /// 상태이상 체크용 프로퍼티 상태이상이 걸리면 점프상태가 해제된다.
    /// </summary>
    protected override bool StateAttack { get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                //상태이상이 true이면 점프상태를 리셋시키고
                //상태이상 상태일때 먼지가 안나오게 한다.
                RollingJumpReset();
                dustTail.SetActive(false);
            }else
            {
                dustTail.SetActive(true);
            }
            

        }
    }

    protected override void Start()
    {
        base.Start();
        aiSpeed = 59.5f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //점프상태가 아닐때만 쿨타임 감소
        if (!rolling)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }

        //쿨타임시간이 0보다 작아지고 상태이상 상태가 아니라면 점프 발동
        if(skillCoolTime<0 && !StateAttack)
        {
            skillCoolTime = skillCoolTimeReset;
            RollingJump();
        }

        //상태이상시 공중에서 빠르게 내려오게하기 위한 조치
        if(StateAttack && transform.position.y>0.15f)
        {
            rigid.AddForce(-transform.up*jumpPower);
        }

    }
    /// <summary>
    /// 빙글빙글 돌면서 점프하는 함수
    /// </summary>
    void RollingJump()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        rolling = true;
        dustTail.SetActive(!rolling);
        animator.SetBool("Rolling", rolling);
    }

    /// <summary>
    /// 점프가 끝났을때 처리해야할 일들을 하는 함수
    /// </summary>
    private void RollingJumpReset()
    {
        rolling = false;
        dustTail.SetActive(!rolling);
        animator.SetBool("Rolling", rolling);
    }

    /// <summary>
    /// 점프상태일때 발자국이 나오지 않도록 조치
    /// </summary>
    protected override void FootPrint()
    {
        if (!rolling)
        {
            base.FootPrint();
        }
    }

    /// <summary>
    /// 점프상태일때 동물을 밣으면 다시한번시전 땅을 밣으면 끝내기위해 override함
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (rolling)
        {
            if(collision.gameObject.CompareTag("Animal")) // 동물일때 다시 점프시전
            {
                RollingJump();
            }


            if (collision.gameObject.CompareTag("Ground")) // 땅일때는 끝
            {
                RollingJumpReset();

            }

        }

    }

    
}
