using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcticFox : AnimalAI
{
    //�ϱؿ����� ��ũ��Ʈ

    float jumpPower = 20.0f; // �󸶳� ���� �������� ����
    bool rolling = false; // ������������ Ȯ���ϴ� ����
    float skillCoolTime = 10.0f; // ��Ÿ�� �ʱ� �ð�
    float skillCoolTimeReset = 10.0f; // ��ų�� �ߵ��ǰ� �ð��� �ʱ�ȭ���� ����

    /// <summary>
    /// �����̻� üũ�� ������Ƽ �����̻��� �ɸ��� �������°� �����ȴ�.
    /// </summary>
    protected override bool StateAttack { get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                //�����̻��� true�̸� �������¸� ���½�Ű��
                //�����̻� �����϶� ������ �ȳ����� �Ѵ�.
                RollingJumpReset();
                dustTail.SetActive(false);
            }else
            {
                dustTail.SetActive(true);
            }
            

        }
    }

    protected override void Start()
    {
        base.Start();
        aiSpeed = 56.5f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //�������°� �ƴҶ��� ��Ÿ�� ����
        if (!rolling)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }

        //��Ÿ�ӽð��� 0���� �۾����� �����̻� ���°� �ƴ϶�� ���� �ߵ�
        if(skillCoolTime<0 && !StateAttack)
        {
            skillCoolTime = skillCoolTimeReset;
            RollingJump();
        }

        //�����̻�� ���߿��� ������ ���������ϱ� ���� ��ġ
        if(StateAttack && transform.position.y>0.15f)
        {
            rigid.AddForce(-transform.up*jumpPower);
        }

    }
    /// <summary>
    /// ���ۺ��� ���鼭 �����ϴ� �Լ�
    /// </summary>
    void RollingJump()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        rolling = true;
        dustTail.SetActive(!rolling);
        animator.SetBool("Rolling", rolling);
    }

    /// <summary>
    /// ������ �������� ó���ؾ��� �ϵ��� �ϴ� �Լ�
    /// </summary>
    private void RollingJumpReset()
    {
        rolling = false;
        dustTail.SetActive(!rolling);
        animator.SetBool("Rolling", rolling);
    }

    /// <summary>
    /// ���������϶� ���ڱ��� ������ �ʵ��� ��ġ
    /// </summary>
    protected override void FootPrint()
    {
        if (!rolling)
        {
            base.FootPrint();
        }
    }

    /// <summary>
    /// ���������϶� ������ �P���� �ٽ��ѹ����� ���� �P���� ���������� override��
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (rolling)
        {
            if(collision.gameObject.CompareTag("Animal")) // �����϶� �ٽ� ��������
            {
                RollingJump();
            }


            if (collision.gameObject.CompareTag("Ground")) // ���϶��� ��
            {
                RollingJumpReset();

            }

        }

    }

    
}
