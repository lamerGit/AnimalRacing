using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : AnimalAI
{
    //여우의 스크립트

    float jumpPower = 20.0f; // 얼마나 높이 점프할지 변수
    bool jumpCheck = false; // 점프상태인지 확인하는 변수
    float skillCoolTime = 4.0f; // 쿨타임 초기 시간
    float skillCoolTimeReset = 4.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    GameObject cfx_Ground_Hit; // 점프 파티클

    /// <summary>
    /// 상태이상 체크용 프로퍼티 상태이상이 걸리면 점프상태가 해제된다.
    /// </summary>
    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                //상태이상이 true이면 점프상태를 리셋시키고
                //상태이상 상태일때 먼지가 안나오게 한다.
                JumpReset();
                dustTail.SetActive(false);
            }
            else
            {
                dustTail.SetActive(true);
            }


        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_Ground_Hit = transform.Find("CFX_Ground_Hit").gameObject;
        cfx_Ground_Hit.SetActive(false);
        //aiSpeed = 59.0f;
        skillCoolTimeReset = Random.Range(1.0f, 9.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.0f, 59.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (raceStarted)
        {
            //점프상태가 아닐때만 쿨타임 감소
            if (!jumpCheck)
            {
                skillCoolTime -= Time.fixedDeltaTime;
            }

            //쿨타임시간이 0보다 작아지고 상태이상 상태가 아니라면 점프 발동
            if (skillCoolTime < 0 && !StateAttack && frontTarget != null)
            {
                skillCoolTime = skillCoolTimeReset;
                Jump();
            }

            //상태이상시 공중에서 빠르게 내려오게하기 위한 조치
            if (StateAttack && transform.position.y > 0.15f)
            {
                rigid.AddForce(-transform.up * jumpPower);
            }
        }

    }
    /// <summary>
    /// 점프하는 함수
    /// </summary>
    void Jump()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        jumpCheck = true;
        cfx_Ground_Hit.SetActive(true);
        dustTail.SetActive(!jumpCheck);
        animator.SetBool("Rolling", jumpCheck);
    }

    /// <summary>
    /// 점프가 끝났을때 처리해야할 일들을 하는 함수
    /// </summary>
    private void JumpReset()
    {
        jumpCheck = false;
        dustTail.SetActive(!jumpCheck);
        animator.SetBool("Rolling", jumpCheck);
    }

    /// <summary>
    /// 점프상태일때 발자국이 나오지 않도록 조치
    /// </summary>
    protected override void FootPrint()
    {
        if (!jumpCheck)
        {
            base.FootPrint();
        }
    }

    /// <summary>
    /// 점프상태일때 땅을 밣으면 끝내기위해 override함
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (jumpCheck)
        {
            
            if (collision.gameObject.CompareTag("Ground")) // 땅일때는 끝
            {
               JumpReset();

            }

        }

    }

}
