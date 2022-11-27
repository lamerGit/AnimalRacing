using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : AnimalAI
{
    //���ȷ� ��ũ��Ʈ

    bool angerRunCheck = false; // �г����ֻ������� Ȯ���ϴ� ����
    float skillCoolTime = 8.0f;  // ��Ÿ�� �ʱ� �ð�
    float skillCoolTimeReset = 8.0f; // ��ų�� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    float tempAngerSpeed = 0.0f; // �г����ֻ����϶� �߰� ���ǵ带 �ӽ÷� ������ ����
    float angerSpeed = 3.0f; // �г����ֻ��� ���ǵ� ��� angerSpeed*������
    float angerTime = 5.0f; // �г����� �����ð�
    float angerTimeReset = 5.0f; // �г����� �����ð� �ʱ�ȭ ����

    
    float sightRange = 15.0f; // ������ ������� �Ÿ��� �ִ��� Ȯ���� ����
    float sightAngle = 90.0f; // ������� ������ �ִ��� Ȯ���� ����

    /// <summary>
    /// �����̻� üũ���� �����̻� �ɸ��� �г����ְ� ��ҵȴ�.
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
        aiSpeed = 59.0f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!angerRunCheck) //�г����� ���°� �ƴ϶�� ��Ÿ�Ӱ���
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }
        else
        {
            //�г����� �����̸� �г����� �ð�����
            angerTime -= Time.fixedDeltaTime;
        }

        if (angerTime < 0)
        {
            //�г����� �ð��� ������ ���¸���
            AngerRunReset();
        }

        if (skillCoolTime < 0 && !StateAttack)
        {
            //��Ÿ���� �ٵǰ� �����̻� ���°� �ƴ϶�� �г����� �ߵ�
            AngerRun();
        }
    }

    /// <summary>
    /// �г����� SearchAnimal�� ���ؼ� �տ��ִ� ������ ������� Ȯ���ϰ� �� ����ŭ �ӵ��� �ø���.
    /// </summary>
    private void AngerRun()
    {
        skillCoolTime = skillCoolTimeReset;
        angerRunCheck = true;
        tempAngerSpeed = angerSpeed * SearchAnimal();
        aiSpeed += tempAngerSpeed;
    }

    /// <summary>
    /// �г����� ���� �ʱ�ȭ
    /// </summary>
    private void AngerRunReset()
    {
        angerTime = angerTimeReset;
        if(angerRunCheck) // �����̻� �ɷ��� ���� ����Ͽ� angerRunCheck�� true�϶��� �ӵ��� ����
        {
            aiSpeed -= tempAngerSpeed;
        }

        angerRunCheck = false;
        
    }
    /// <summary>
    /// ���ȷξտ� ����� ������ �ִ��� Ȯ���ϰ� �� ���� �����ϴ� �Լ�
    /// </summary>
    /// <returns>�տ� �ִ� ������ ��</returns>
    int SearchAnimal()
    {
        //�ڱ� �ڽ� �����ؼ� ����ã�´�.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        int count = 0;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //Ư������ �ȿ� �ִ��� Ȯ���Ѵ�.
                if (InSightAngle(colliders[i].transform.position))
                {

                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// Ÿ�ٰ� ���� ������ üũ�ϴ� �Լ�
    /// </summary>
    /// <param name="targetPosition">Ÿ���� ��ġ</param>
    /// <returns>�����ȿ� ������ true �ƴϸ� false</returns>
    bool InSightAngle(Vector3 targetPosition)
    {
        //Vector3.Angle�� ���� Ÿ�ٰ� ���� ������ Ȯ��
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);

        //sightAngle�� 90.0f�̸� *0.5f�� ���ϸ� 45.0f�̴� 45.0f���� ������ 45�� �ȿ� �ִ� ���̴�.
        return (sightAngle * 0.5f) > angle;
    }
}
