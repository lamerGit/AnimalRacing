using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donkey : AnimalAI
{
    bool noiseCheck = false;
    float skillCoolTime = 8.0f;
    float skillCoolTimeReset = 8.0f;

    float noisePower = 3.0f;
    
    float sightRange = 10.0f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!noiseCheck)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }
        


        if (skillCoolTime < 0 && !StateAttack)
        {
            //Debug.Log("노이즈 발동");
            Noise();
        }
    }
    protected override void Start()
    {
        base.Start();
        aiSpeed = 58.5f;
    }

    private void Noise()
    {
        skillCoolTime = skillCoolTimeReset;
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != transform.gameObject)
                {

                    colliders[i].gameObject.GetComponent<IHit>().TakeHit(noisePower, HitType.silence);
                }

            }
        }
    }

}
