using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ox : AnimalAI
{
    //Ȳ���� ��ũ��Ʈ
    
    bool madSpin=false; //��ģȸ���� �ߵ������� Ȯ���ϴ� ����
    float skillCoolTime = 8.0f; //��Ÿ�� �ʱ� �ð�
    float skillCoolTimeReset = 10.0f; // ��ų�� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    float madSpinSpeed = 5.0f; //��ģȸ�� �����϶� ����ϴ� ���ǵ�
    float madSpinTime = 5.0f; // ��ģȸ�� �����Ǵ� �ð�
    float madSpinTimeReset = 5.0f; // ��ģȸ���� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    float madPower = 20.0f; // ��ģȸ���� ���� �������� ���ҵ� ���ǵ�

    /// <summary>
    /// �����̻� üũ�� ������Ƽ �����̻��� �ɸ��� ��ģȸ���� ��ҵȴ�.
    /// </summary>
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

        //��ģȸ�� ���°� �ƴҶ��� ��Ÿ�Ӱ���
        if (!madSpin)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }else
        {
            //��ģȸ�� �����϶��� �����ð� ����
            madSpinTime -= Time.fixedDeltaTime;
        }

        if(madSpinTime<0)
        {
            //���� �ð��� ���� �����ϸ� ���¸���
            MadnessSpinReset();
        }

        if (skillCoolTime < 0 && !StateAttack)
        {
           //�����̻��� �ƴϰ� ��ų ��Ÿ���� 0���� �۾����� �ߵ�
            MadnessSpin();
        }
    }

    /// <summary>
    /// ��ģȸ�������϶��� ȸ������ �ʰ� �ߴ޷��������� Sensor�� ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    protected override void Sensor()
    {
        if (!madSpin)
        {
            base.Sensor();
        }
    }

    /// <summary>
    /// ��ģȸ���� ������ ���¸� �����ϴ� �Լ�
    /// </summary>
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

    /// <summary>
    /// ��ģȸ���� �����ϴ� �Լ�
    /// </summary>
    void MadnessSpin()
    {
        skillCoolTime = skillCoolTimeReset;
        madSpin =true;
        aiSpeed += madSpinSpeed;
        animator.SetBool("MadnessSpin", madSpin);
    }

    /// <summary>
    /// ��ģȸ�������϶� �ٸ� ������� �����ϸ� �����̻� ������ �ɰԵȴ�.
    /// </summary>
    /// <param name="collision"></param>
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
