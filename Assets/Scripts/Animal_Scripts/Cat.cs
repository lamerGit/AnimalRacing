using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : AnimalAI
{
    float jumpPower = 5.0f; // 얼마나 높이 점프할지 변수
    bool jumpCheck = false; // 점프상태인지 확인하는 변수
    float skillCoolTime = 4.0f; // 쿨타임 초기 시간
    float skillCoolTimeReset = 4.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float jumpTime = 8.0f;
    float jumpTimeReset = 8.0f;

    float extraSpeed = 5.0f;

    GameObject cfx_RainbowTail;

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
               
                JumpReset();
                dustTail.SetActive(false);
                cfx_RainbowTail.SetActive(false);
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

        cfx_RainbowTail = transform.Find("RainBowTail").gameObject;
        cfx_RainbowTail.SetActive(false);

        skillCoolTimeReset = Random.Range(35.0f, 40.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            //점프상태가 아닐때만 쿨타임 감소
            if (!jumpCheck)
            {
                skillCoolTime -= Time.fixedDeltaTime;
            }else if(jumpCheck && !rigid.useGravity)
            {
                jumpTime -= Time.fixedDeltaTime;
            }

            //쿨타임시간이 0보다 작아지고 상태이상 상태가 아니라면 점프 발동
            if (skillCoolTime < 0 && !StateAttack)
            {
                skillCoolTime = skillCoolTimeReset;
                Jump();
            }

            if(jumpTime<0)
            {
                jumpTime = jumpTimeReset;
                rigid.useGravity = true;
                cfx_RainbowTail.SetActive(false);
               
                //JumpReset();
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
        
        animalAudio.Play();
        rigid.useGravity = false;
        jumpCheck = true;
        aiSpeed += extraSpeed;
        animator.SetBool("CatFly", jumpCheck);
        cfx_RainbowTail.SetActive(jumpCheck);
        dustTail.SetActive(!jumpCheck);
    }

    /// <summary>
    /// 점프가 끝났을때 처리해야할 일들을 하는 함수
    /// </summary>
    private void JumpReset()
    {
        if(jumpCheck)
        {
            aiSpeed -= extraSpeed;
        }

        jumpCheck = false;
     
        animalAudio.Stop();
        animator.SetBool("CatFly", jumpCheck);
        dustTail.SetActive(!jumpCheck);
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
