﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donkey : AnimalAI
{
    //당나귀 스크립트


    bool noiseCheck = false; //시끄러운 소리 시전중인지 확인하는 변수
    float skillCoolTime = 16.0f; // 쿨타임 초기 시간
    float skillCoolTimeReset = 16.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float noisePower = 3.0f; // 스킬에 공격당한 동물들의 감속을 결정하느 변수
    
    float sightRange = 10.0f; // 스킬의 범위

    GameObject cfx_Heehaw;
    Transform[] cfx_Heehaw_child;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (RaceStarted)
        {
            if (!noiseCheck)
            {
                //시끄러운 소리가 시전중이지 아닐때 쿨타임 감소
                skillCoolTime -= Time.fixedDeltaTime;
            }



            if (skillCoolTime < 0 && !StateAttack)
            {
                //쿨타임이 0보다작고 상태이상이 아닐때 발동
                Noise();
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        cfx_Heehaw = transform.Find("CFXR2 _HeeHaw_").gameObject;
        cfx_Heehaw_child = cfx_Heehaw.transform.GetComponentsInChildren<Transform>();
        cfx_Heehaw.SetActive(false);


        //aiSpeed = 61.0f;
        skillCoolTimeReset = Random.Range(11.0f, 21.0f);
        skillCoolTime = skillCoolTimeReset;
        aiSpeed = Random.Range(59.0f, 61.0f);
    }

    /// <summary>
    /// 시끄러운소리 주변에 있는 동물들을 침묵시키고 속도를 감소시킨다.
    /// </summary>
    private void Noise()
    {
        skillCoolTime = skillCoolTimeReset;
        animalAudio.Play();
        cfx_Heehaw.SetActive(true);
        for(int i=0; i<cfx_Heehaw_child.Length; i++)
        {
            cfx_Heehaw_child[i].gameObject.SetActive(true);
        }

        //나를 포함에 범위안에 모든 동물을 찾는다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //나를 제외한 동물들을 전부 침묵시키고 감속시킨다.
                if (colliders[i].gameObject != transform.gameObject)
                {
                    aiSpeed += 0.2f;
                    colliders[i].gameObject.GetComponent<IHit>().TakeHit(noisePower, HitType.silence);
                }

            }
        }
    }

}
 