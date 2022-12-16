using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : AnimalAI
{
    GameObject cfx_elephant;
    int angleStack = 0;
    int angleMaxStack = 2;

    float angleTimeReset = 6.0f;
    float angleTime = 6.0f;

    bool angle = false;

    float power = 20.0f;
    float angleSpeed = 5.0f;
    int AngleStack
    {
        get { return angleStack; }
        set
        {
            angleStack = value;
            if (angleStack == angleMaxStack)
            {
                angleStack = 0;
                angle = true;
                aiSpeed += angleSpeed+1.0f;
                animalAudio.Play();
                cfx_elephant.SetActive(true);

            }


        }
    }

    protected override void Start()
    {
        base.Start();
        cfx_elephant = transform.Find("CFX_Elephant").gameObject;
        cfx_elephant.SetActive(false);
        angleTimeReset = Random.Range(4.0f, 8.0f);
        angleTime = angleTimeReset;

        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(angle)
        {
            angleTime -= Time.fixedDeltaTime;
        }

        if(angleTime<0)
        {
            AngleReset();
        }

    }

    void AngleReset()
    {
        angle = false;
        angleTime = angleTimeReset;
        aiSpeed -= angleSpeed;
        cfx_elephant.SetActive(false);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!angle)
        {
            base.OnCollisionEnter(collision);
        }else
        {
            if(collision.gameObject.CompareTag("Animal"))
            {
                collision.gameObject.GetComponent<AnimalAI>().TakeHit(power, HitType.Spin);
            }
        }

    }

    public override void TakeHit(float stateDamage, HitType hitType = HitType.None)
    {
        //base.TakeHit(stateDamage, hitType);
        //Debug.Log("압도적인 힘에는 소용이 없었따!");
        if (!angle)
        {
            AngleStack++;
        }
    }
}
