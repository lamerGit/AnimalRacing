using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bighorn : AnimalAI
{
    //큰뿔양의 스크립트

    //bool explosionCheck = false; //양폭탄 시전중인지 확인하는 변수
    //float skillCoolTime = 16.0f; // 쿨타임 초기 시간
    //float skillCoolTimeReset = 16.0f; // 스킬이 발동되고 시간을 초기화해줄 변수

    float explosionPower = 20.0f; // 스킬에 공격당한 동물들의 감속을 결정하느 변수

    float sightRange = 10.0f; // 스킬의 범위


    GameObject cfx_Explosion; // 양폭탄 파티클

    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (!base.StateAttack)
            {
                cfx_Explosion.SetActive(true);
                Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
                if (colliders.Length > 0)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        //나를 제외한 동물들을 전부 에어본시키고 감속시킨다.
                        if (colliders[i].gameObject != transform.gameObject)
                        {

                            colliders[i].gameObject.GetComponent<IHit>().TakeHit(explosionPower, HitType.airborne);
                        }

                    }
                }
            }
            


        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_Explosion = transform.Find("CFX_Explosion").gameObject;
        cfx_Explosion.SetActive(false);
    }
}
