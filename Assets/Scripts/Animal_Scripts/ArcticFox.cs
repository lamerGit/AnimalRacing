using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcticFox : AnimalAI
{
    float jumpPower = 20.0f;
    bool rolling = false;
    float skillCoolTime = 10.0f;
    float skillCoolTimeReset = 10.0f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!rolling)
        {
            skillCoolTime -= Time.fixedDeltaTime;
        }
        if(skillCoolTime<0)
        {
            skillCoolTime = skillCoolTimeReset;
            RollingJump();
        }

    }
    void RollingJump()
    {
        rigid.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        rolling = true;
        dustTail.SetActive(!rolling);
        animator.SetBool("Rolling", rolling);
    }

    protected override void FootPrint()
    {
        if (!rolling)
        {
            base.FootPrint();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (rolling)
        {
            if(collision.gameObject.CompareTag("Animal"))
            {
                RollingJump();
            }


            if (collision.gameObject.CompareTag("Ground"))
            {

                rolling = false;
                dustTail.SetActive(!rolling);
                animator.SetBool("Rolling", rolling);

            }

        }

    }
}
