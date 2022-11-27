using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donkey : AnimalAI
{
    //�糪�� ��ũ��Ʈ


    bool noiseCheck = false; //�ò����� �Ҹ� ���������� Ȯ���ϴ� ����
    float skillCoolTime = 16.0f; // ��Ÿ�� �ʱ� �ð�
    float skillCoolTimeReset = 16.0f; // ��ų�� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    float noisePower = 3.0f; // ��ų�� ���ݴ��� �������� ������ �����ϴ� ����
    
    float sightRange = 10.0f; // ��ų�� ����

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!noiseCheck)
        {
            //�ò����� �Ҹ��� ���������� �ƴҶ� ��Ÿ�� ����
            skillCoolTime -= Time.fixedDeltaTime;
        }
        


        if (skillCoolTime < 0 && !StateAttack)
        {
            //��Ÿ���� 0�����۰� �����̻��� �ƴҶ� �ߵ�
            Noise();
        }
    }
    protected override void Start()
    {
        base.Start();
        aiSpeed = 58.5f;
    }

    /// <summary>
    /// �ò�����Ҹ� �ֺ��� �ִ� �������� ħ����Ű�� �ӵ��� ���ҽ�Ų��.
    /// </summary>
    private void Noise()
    {
        skillCoolTime = skillCoolTimeReset;


        //���� ���Կ� �����ȿ� ��� ������ ã�´�.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Animal"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //���� ������ �������� ���� ħ����Ű�� ���ӽ�Ų��.
                if (colliders[i].gameObject != transform.gameObject)
                {

                    colliders[i].gameObject.GetComponent<IHit>().TakeHit(noisePower, HitType.silence);
                }

            }
        }
    }

}
