using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ox : AnimalAI
{
    
    bool madSpin=false;
    float skillCoolTime = 8.0f;
    float skillCoolTimeReset = 10.0f;

    float madSpinSpeed = 5.0f;
    float madSpinTime = 5.0f;
    float madSpinTimeReset = 5.0f;

    float madPower = 20.0f;

    protected override bool StateAttack { get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                MadnessSpinReset();
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

        if (!madSpin)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }else
        {
            madSpinTime -= Time.fixedDeltaTime;
        }

        if(madSpinTime<0)
        {
            
            MadnessSpinReset();
        }

        if (skillCoolTime < 0 && !StateAttack)
        {
           
            MadnessSpin();
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
        if(madSpin)
        {
            aiSpeed -= madSpinSpeed;
        }

        madSpin = false;
        
        animator.SetBool("MadnessSpin", madSpin);
    }

    void MadnessSpin()
    {
        skillCoolTime = skillCoolTimeReset;
        madSpin =true;
        aiSpeed += madSpinSpeed;
        animator.SetBool("MadnessSpin", madSpin);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if(madSpin)
        {
            if(collision.gameObject.CompareTag("Animal"))
            {
                collision.gameObject.GetComponent<IHit>().TakeHit(madPower,HitType.Spin);
            }
        }
    }
}
