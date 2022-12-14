using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : AnimalAI
{
    float skillCoolTime = 15.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 15.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float extraSpeed = 1.0f; //  추가스피드

    float sightRange = 10.0f; // 주변 탐색범위 변수

    protected override void Start()
    {
        base.Start();

        skillCoolTimeReset = Random.Range(5.0f, 10.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            skillCoolTime -= Time.fixedDeltaTime;

            // 쿨타임이 되고 상태이상이 아니며 발동
            if (skillCoolTime < 0 && !StateAttack)
            {

                ExcitRun();
            }
        }



    }

    /// <summary>
    /// 주변 동물 확인하고 3마리 이상이면 스피드상승
    /// </summary>
    void ExcitRun()
    {
        skillCoolTime = skillCoolTimeReset;
        
        if (AnimalCheck())
        {
            animalAudio.Play();
            animator.SetTrigger("DogJump");
            aiSpeed += extraSpeed;
        }
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
            if (count > 3)
            {
                result = true;
            }
        }


        return result;
    }
}
