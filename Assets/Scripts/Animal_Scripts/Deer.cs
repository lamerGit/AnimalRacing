using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : AnimalAI
{
    //사슴의 스크립트 

    float skillCoolTime = 5.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 5.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float attackSpeed = 10.0f; // 뿔올려치기 시전중에 추가스피드
    float attackTime = 5.0f;  // 뿔올려치기 시전 시간
    float attackTimeReset = 5.0f; // 뿔올려치기가 발동되고 시간을 초기화해줄 변수

    bool attackCheck=false; // 뿔올려치기중인지 확인할 변수

    float attackDistance = 8.5f; // 어느정도 거리에 있어야 뿔올려치기를 할지 정하는 변수
    float attackPower = 25.0f; // 에어본상태의 동물의 스피드가 얼마나 떨어질지 정하는 변수

    GameObject cfx_Hit;
    Transform[] cfx_Hit_Child;

    /// <summary>
    /// 상태이상 체크용 프로퍼티 상태이상이 걸리면 뿔올려치기가 취소된다.
    /// </summary>
    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                UpAttackReset();
            }


        }
    }
    protected override void Start()
    {
        base.Start();
        cfx_Hit = transform.Find("CFX_Hit").gameObject;
        cfx_Hit_Child = cfx_Hit.GetComponentsInChildren<Transform>();
        cfx_Hit.SetActive(false);

        skillCoolTimeReset = Random.Range(1.0f, 10.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.5f, 60.5f);
        //aiSpeed = 59.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (raceStarted)
        {

            if (!attackCheck)
            {
                //뿔올려치기 상태가 아닐때는 쿨타임감소
                skillCoolTime -= Time.fixedDeltaTime;
            }
            else
            {
                //뿔올려치기 상태일때 앞에 타겟이 있을 경우 거리를 재다가 올려친다.
                attackTime -= Time.fixedDeltaTime;
                if (frontTarget != null)
                {
                    float distance = Vector3.SqrMagnitude(transform.position - frontTarget.position);

                    if (distance < attackDistance)
                    {

                        animator.SetTrigger("UpAttack");
                        cfx_Hit.SetActive(true);
                        for (int i = 0; i < cfx_Hit_Child.Length; i++)
                        {
                            cfx_Hit_Child[i].gameObject.SetActive(true);
                        }
                        frontTarget.gameObject.GetComponent<AnimalAI>().TakeHit(attackPower, HitType.airborne);
                        UpAttackReset(); // 올려친 이후 상태 리셋
                    }
                }
            }

            if (attackTime < 0)
            {
                //뿔올려치기 상태가 끝나면 상태 리셋
                UpAttackReset();
            }

            // 쿨타임이 되고 상태이상이 아니며 앞에 동물이 있으면 발동
            if (skillCoolTime < 0 && !StateAttack && frontTarget != null)
            {

                UpAttack();
            }
        }



    }

    protected override void AvoidSteer(float senstivity)
    {
        if (!attackCheck)
        {
            base.AvoidSteer(senstivity);
        }
    }

    /// <summary>
    /// 뿔올려치기 상태와 속도만 조절하는 함수
    /// </summary>
    void UpAttack()
    {
        skillCoolTime = skillCoolTimeReset;
        attackCheck = true;
        aiSpeed += attackSpeed;
    }

    /// <summary>
    /// 뿔올려치기후 상태 리셋
    /// </summary>
    void UpAttackReset()
    {
        attackTime = attackTimeReset;
        if(attackCheck) // 올려치기가 true일때만 속도를 감소시켜야한다.
        {               // 이렇게 하지않으면 상태이상걸렸을때 속도가 감소한다.
            aiSpeed -= attackSpeed;
        }

        attackCheck = false;
        
    }
}
