using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : AnimalAI
{
    float skillCoolTime = 10.0f;
    float skillCoolTimeReset = 10.0f;

    float attackSpeed = 10.0f;
    float attackTime = 5.0f;
    float attackTimeReset = 5.0f;

    bool attackCheck=false;

    float attackDistance = 8.5f;
    float attackPower = 25.0f;

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
        aiSpeed = 58.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!attackCheck)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }else
        {
            attackTime -= Time.fixedDeltaTime;
            if (frontTarget != null)
            {
                float distance = Vector3.SqrMagnitude(transform.position - frontTarget.position);
    
                if(distance<attackDistance)
                {
                    
                    animator.SetTrigger("UpAttack");
                    frontTarget.gameObject.GetComponent<AnimalAI>().TakeHit(attackPower, HitType.airborne);
                    UpAttackReset();
                }
            }
        }

        if (attackTime < 0)
        {
            UpAttackReset();
        }


        if (skillCoolTime < 0 && !StateAttack && frontTarget!=null)
        {
            
            UpAttack();
        }



    }

    void UpAttack()
    {
        skillCoolTime = skillCoolTimeReset;
        attackCheck = true;
        aiSpeed += attackSpeed;
    }

    void UpAttackReset()
    {
        attackTime= attackTimeReset;
        attackCheck=false;
        aiSpeed -= attackSpeed;
    }
}
