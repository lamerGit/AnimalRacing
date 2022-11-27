using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : AnimalAI
{
    bool angerRunCheck = false;
    float skillCoolTime = 8.0f;
    float skillCoolTimeReset = 8.0f;

    float tempAngerSpeed = 0.0f;
    float angerSpeed = 2.0f;
    float angerTime = 5.0f;
    float angerTimeReset = 5.0f;

    
    float sightRange = 15.0f;
    float sightAngle = 90.0f;

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
        aiSpeed = 59.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!angerRunCheck)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }
        else
        {
            angerTime -= Time.fixedDeltaTime;
        }

        if (angerTime < 0)
        {

            AngerRunReset();
        }

        if (skillCoolTime < 0 && !StateAttack)
        {

            AngerRun();
        }
    }

    private void AngerRun()
    {
        skillCoolTime = skillCoolTimeReset;
        angerRunCheck = true;
        tempAngerSpeed = angerSpeed * SearchAnimal();
        aiSpeed += tempAngerSpeed;
    }

    private void AngerRunReset()
    {
        angerTime = angerTimeReset;
        if(angerRunCheck)
        {
            aiSpeed -= tempAngerSpeed;
        }

        angerRunCheck = false;
        
    }

    int SearchAnimal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        int count = 0;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {

                if (InSightAngle(colliders[i].transform.position))
                {

                    count++;
                }
            }
        }

        return count;
    }

    bool InSightAngle(Vector3 targetPosition)
    {
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);

        return (sightAngle * 0.5f) > angle;
    }
}
