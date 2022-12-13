
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : AnimalAI
{
    //버팔로 스크립트
      
    bool angerRunCheck = false; // 분노질주상태인지 확인하는 변수
    float skillCoolTime = 12.0f;  // 쿨타임 초기 시간
    float skillCoolTimeReset = 12.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float tempAngerSpeed = 0.0f; // 분노질주상태일때 추가 스피드를 임시로 저장할 변수
    float angerSpeed = 3.0f; // 분노질주상태 스피드 계수 angerSpeed*동물수
    float angerTime = 5.0f; // 분노질주 시전시간
    float angerTimeReset = 5.0f; // 분노질주 시전시간 초기화 변수

    
    float sightRange = 15.0f; // 동물이 어느정도 거리에 있는지 확인할 변수
    float sightAngle = 90.0f; // 어느정도 각도에 있는지 확인할 변수

    GameObject cfx_WaterFire;

    /// <summary>
    /// 상태이상 체크변수 상태이상에 걸리면 분노질주가 취소된다.
    /// </summary>
    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                AngerRunReset();
            }


        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_WaterFire = transform.Find("CFX4_WaterFire").gameObject;
        cfx_WaterFire.SetActive(false);

        //aiSpeed = 59.0f;
        skillCoolTimeReset = Random.Range(7.0f, 17.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            if (!angerRunCheck) //분노질주 상태가 아니라면 쿨타임감소
            {
                skillCoolTime -= Time.fixedDeltaTime;
            }
            else
            {
                //분노질주 상태이면 분노질주 시간감소
                angerTime -= Time.fixedDeltaTime;
            }

            if (angerTime < 0)
            {
                //분노질주 시간이 끝나면 상태리셋
                AngerRunReset();
            }

            if (skillCoolTime < 0 && !StateAttack)
            {
                //쿨타임이 다되고 상태이상 상태가 아니라면 분노질주 발동
                AngerRun();
            }
        }
    }

    /// <summary>
    /// 분노질주 SearchAnimal을 통해서 앞에있는 동물이 몇마리인지 확인하고 그 수만큼 속도를 올린다.
    /// </summary>
    private void AngerRun()
    {
        skillCoolTime = skillCoolTimeReset;
        animalAudio.Play();
        angerRunCheck = true;
        cfx_WaterFire.SetActive(angerRunCheck);
        tempAngerSpeed = angerSpeed * SearchAnimal();
        aiSpeed += tempAngerSpeed;
    }

    /// <summary>
    /// 분노질주 상태 초기화
    /// </summary>
    private void AngerRunReset()
    {
        angerTime = angerTimeReset;
        if(angerRunCheck) // 상태이상 걸렸을 때를 대비하여 angerRunCheck가 true일때만 속도를 감소
        {
            aiSpeed -= tempAngerSpeed;
        }
        
        angerRunCheck = false;
        cfx_WaterFire.SetActive(angerRunCheck);

    }
    /// <summary>
    /// 버팔로앞에 몇마리의 동물이 있는지 확인하고 그 수를 리턴하는 함수
    /// </summary>
    /// <returns>앞에 있는 동물의 수</returns>
    int SearchAnimal()
    {
        //자기 자신 포함해서 전부찾는다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        int count = 0;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //특정각도 안에 있는지 확인한다.
                if (InSightAngle(colliders[i].transform.position))
                {

                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// 타겟과 나의 각도를 체크하는 함수
    /// </summary>
    /// <param name="targetPosition">타겟의 위치</param>
    /// <returns>각도안에 있으면 true 아니면 false</returns>
    bool InSightAngle(Vector3 targetPosition)
    {
        //Vector3.Angle을 통해 타겟과 나의 각도를 확인
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);

        //sightAngle은 90.0f이면 *0.5f를 곱하면 45.0f이다 45.0f보다 작으면 45도 안에 있는 것이다.
        return (sightAngle * 0.5f) > angle;
    }
}
