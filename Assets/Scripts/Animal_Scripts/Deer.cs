using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : AnimalAI
{
    //�罿�� ��ũ��Ʈ

    float skillCoolTime = 5.0f; //��Ÿ�� �ʱ� �ð�
    float skillCoolTimeReset = 5.0f; // ��ų�� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    float attackSpeed = 10.0f; // �Կ÷�ġ�� �����߿� �߰����ǵ�
    float attackTime = 5.0f;  // �Կ÷�ġ�� ���� �ð�
    float attackTimeReset = 5.0f; // �Կ÷�ġ�Ⱑ �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    bool attackCheck=false; // �Կ÷�ġ�������� Ȯ���� ����

    float attackDistance = 8.5f; // ������� �Ÿ��� �־�� �Կ÷�ġ�⸦ ���� ���ϴ� ����
    float attackPower = 25.0f; // ��������� ������ ���ǵ尡 �󸶳� �������� ���ϴ� ����

    /// <summary>
    /// �����̻� üũ�� ������Ƽ �����̻��� �ɸ��� �Կ÷�ġ�Ⱑ ��ҵȴ�.
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
        aiSpeed = 59.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!attackCheck)
        {
            //�Կ÷�ġ�� ���°� �ƴҶ��� ��Ÿ�Ӱ���
            skillCoolTime -= Time.fixedDeltaTime;
        }else
        {
            //�Կ÷�ġ�� �����϶� �տ� Ÿ���� ���� ��� �Ÿ��� ��ٰ� �÷�ģ��.
            attackTime -= Time.fixedDeltaTime;
            if (frontTarget != null)
            {
                float distance = Vector3.SqrMagnitude(transform.position - frontTarget.position);
    
                if(distance<attackDistance)
                {
                    
                    animator.SetTrigger("UpAttack");
                    frontTarget.gameObject.GetComponent<AnimalAI>().TakeHit(attackPower, HitType.airborne);
                    UpAttackReset(); // �÷�ģ ���� ���� ����
                }
            }
        }

        if (attackTime < 0)
        {
            //�Կ÷�ġ�� ���°� ������ ���� ����
            UpAttackReset();
        }

        // ��Ÿ���� �ǰ� �����̻��� �ƴϸ� �տ� ������ ������ �ߵ�
        if (skillCoolTime < 0 && !StateAttack && frontTarget!=null)
        {
            
            UpAttack();
        }



    }

    /// <summary>
    /// �Կ÷�ġ�� ���¿� �ӵ��� �����ϴ� �Լ�
    /// </summary>
    void UpAttack()
    {
        skillCoolTime = skillCoolTimeReset;
        attackCheck = true;
        aiSpeed += attackSpeed;
    }

    /// <summary>
    /// �Կ÷�ġ���� ���� ����
    /// </summary>
    void UpAttackReset()
    {
        attackTime = attackTimeReset;
        if(attackCheck) // �÷�ġ�Ⱑ true�϶��� �ӵ��� ���ҽ��Ѿ��Ѵ�.
        {               // �̷��� ���������� �����̻�ɷ����� �ӵ��� �����Ѵ�.
            aiSpeed -= attackSpeed;
        }

        attackCheck = false;
        
    }
}
