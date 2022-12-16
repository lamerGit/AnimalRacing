using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : AnimalAI
{
    int coin = 0;
    float coinTime = 5.0f;
    float coinTimeReset = 5.0f;

    float coinSpeed = 0.7f;

    GameObject cfxcoin;
    int Coin
    {
        get { return coin; }
        set
        {
            int temp = coin;
            coin = value;
            if (coin == 0)
            {
                aiSpeed -= temp * coinSpeed;
                //Debug.Log("스피드 감소");
            }
            else
            {
                aiSpeed += coinSpeed;
                //Debug.Log("스피드 증가");
            }



        }
    }


    protected override bool StateAttack
    {
        get => base.StateAttack;
        set
        {
            base.StateAttack = value;
            if (base.StateAttack)
            {
                Coin = 0;
            }
            

        }
    }

    protected override void Start()
    {
        base.Start();
        cfxcoin = transform.Find("CFXR2 Coin").gameObject;
        cfxcoin.SetActive(false);
        coinTimeReset = Random.Range(4.0f, 8.0f);
        coinTime = coinTimeReset;

        aiSpeed = Random.Range(58.0f, 60.0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(RaceStarted)
        {
            coinTime-=Time.fixedDeltaTime;

            if (coinTime < 0)
            {
               
                CoinReset();
            }
        }
    }

    void CoinReset()
    {
        coinTime = coinTimeReset;
        cfxcoin.SetActive(true);
        animalAudio.Play();
        Coin++;
    }
}
