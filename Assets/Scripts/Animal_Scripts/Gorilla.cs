using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorilla : AnimalAI
{

    bool madSpin = false; //미친회전이 발동중인지 확인하는 변수
    float skillCoolTime = 8.0f; //쿨타임 초기 시간
    float skillCoolTimeReset = 8.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float madSpinSpeed = 10.0f; //미친회전 상태일때 상승하는 스피드
    float madSpinTime = 1.5f; // 미친회전 시전되는 시간
    float madSpinTimeReset = 1.5f; // 미친회전이 발동되고 시간을 초기화해줄 변수

    float madPower = 20.0f; // 미친회전에 당한 동물들이 감소될 스피드

    protected override void Start()
    {
        base.Start();
       

        skillCoolTimeReset = Random.Range(5.0f, 10.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(58.5f, 60.5f);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            //미친회전 상태가 아닐때만 쿨타임감수
            if (!madSpin)
            {
                skillCoolTime -= Time.fixedDeltaTime;
            }
            else
            {
                //미친회전 상태일때는 시전시간 감소
                madSpinTime -= Time.fixedDeltaTime;
            }

            if (madSpinTime < 0)
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
    }
    protected override void Sensor()
    {
        if (!madSpin)
        {
            base.Sensor();
        }
    }
    private void MadnessSpinReset()
    {
        madSpinTime = madSpinTimeReset;
        if (madSpin)
        {
            aiSpeed -= madSpinSpeed;
        }

        animalAudio.Stop();
        madSpin = false;
        animator.SetBool("GorillaSpin", madSpin);
    }

    void MadnessSpin()
    {
        skillCoolTime = skillCoolTimeReset;
        animalAudio.Play();
        madSpin = true;
        aiSpeed += madSpinSpeed;
        animator.SetBool("GorillaSpin", madSpin);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (madSpin)
        {
            if (collision.gameObject.CompareTag("Animal"))
            {
                collision.gameObject.GetComponent<IHit>().TakeHit(madPower, HitType.Spin);
                MadnessSpinReset();
            }
        }
    }

}
