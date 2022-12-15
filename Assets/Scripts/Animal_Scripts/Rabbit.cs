using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : AnimalAI
{
    int level = 0;
    int maxLevel = 7;

    float jumpPower = 15.0f; // 얼마나 높이 점프할지 변수
    bool jumpCheck = false; // 점프상태인지 확인하는 변수
    float levelUpTime = 5.0f; // 쿨타임 초기 시간
    float levelUpTimeReset = 5.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float skillCoolTime = 2.0f; // 쿨타임 초기 시간
    float skillCoolTimeReset = 2.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float extraSpeed = 7.0f;

    bool maxLevelCheck=false;

    GameObject cfx_Ground_Hit; // 점프 파티클
    GameObject cfx_levelUp;
    GameObject cfx_MaxLevel;
    GameObject cfx_MaxLevelLight;


    int Level
    {
        get { return level; }
        set
        {
            level = value;
            if (level == maxLevel)
            {
                MaxLevelRabbit();
            }else
            {
                if (level < maxLevel)
                {
                    cfx_levelUp.SetActive(false);
                    cfx_levelUp.SetActive(true);
                }
            }


        }
    }

    

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
                Level++;
                dustTail.SetActive(true);
            }


        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_Ground_Hit = transform.Find("CFX_Ground_Hit").gameObject;
        cfx_Ground_Hit.SetActive(false);
        cfx_levelUp = transform.Find("CFXR_LevelUp").gameObject;
        cfx_levelUp.SetActive(false);
        cfx_MaxLevel = transform.Find("CFXR_MaxLevel").gameObject;
        cfx_MaxLevel.SetActive(false);
        cfx_MaxLevelLight = transform.Find("CFXR3_MaxLevelLight").gameObject;
        cfx_MaxLevelLight.SetActive(false);

        skillCoolTimeReset = Random.Range(6.0f, 9.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(56.5f, 58.5f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            if (!maxLevelCheck)
            {
                levelUpTime -= Time.fixedDeltaTime;
            }

            if (!jumpCheck)
            {
                skillCoolTime -= Time.fixedDeltaTime;
            }


            if (levelUpTime<0)
            {
                levelUpTime = levelUpTimeReset;
                Level++;
            }
            
            if (skillCoolTime < 0 && frontTarget != null && maxLevelCheck)
            {
                skillCoolTime = skillCoolTimeReset;
                Jump();
            }

        }

    }

    private void MaxLevelRabbit()
    {
        aiSpeed += extraSpeed;
        maxLevelCheck = true;
        cfx_MaxLevel.SetActive(true);
        cfx_MaxLevelLight.SetActive(true);
        StartCoroutine(MaxLevelCfxClose());
        //Debug.Log("만렙입니다");
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

    void Jump()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        animalAudio.Play();
        jumpCheck = true;
        cfx_Ground_Hit.SetActive(true);
        dustTail.SetActive(!jumpCheck);
        animator.SetBool("Rolling", jumpCheck);
    }

    private void JumpReset()
    {
        jumpCheck = false;
        dustTail.SetActive(!jumpCheck);
        animator.SetBool("Rolling", jumpCheck);
    }

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

    IEnumerator MaxLevelCfxClose()
    {
        yield return new WaitForSeconds(2.0f);
        cfx_MaxLevel.SetActive(false);
    }

    public override void TakeHit(float stateDamage, HitType hitType = HitType.None)
    {
        if (!maxLevelCheck)
        {
            base.TakeHit(stateDamage, hitType);
        }
        
    }

}
