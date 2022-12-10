using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    //게임매니저 스크립트 싱글톤을 상속받는다.


    int[] animalNumbers=new int[10] {0,1,2,3,4,5,6,7,8,9}; //레이스에 참가하는 동물들
    bool produceCheck = false; //레이스가 생성됬는지 확인하는 변수
    int animalCount = 6; //레이스에 참가하는 동물의 수

    Player gamePlayer;

    public AnimalData[] animalDatas; // 동물들의 스크립트오브젝트를 받는다.

    int ticketCount = 0;
    TicketData[] ticketDatas=new TicketData[10];
    bool[] ticketSuccess=new bool[10] {false, false, false, false, false, false, false, false, false, false };

    public static int MAXTICKETCOUNT = 10;

    public int TicketCount
    {
        get { return ticketCount; }
        set { ticketCount = value; }
    }

    public TicketData[] TicketDatas
    {
        get { return ticketDatas; }
        set { ticketDatas = value; }

    }

    public bool[] TicketSuccess
    {
        get { return ticketSuccess; }
        set { ticketSuccess = value; }
    }


    public Player GamePlayer
    {
        get { return gamePlayer; }
    }

    /// <summary>
    /// 레이스의 참가하는 동물들의 프로퍼티
    /// </summary>
    public int[] AnimalNumbers
    {
        get { return animalNumbers; }
        set { animalNumbers = value; }
    }

    /// <summary>
    /// 레이스생성 여부를 알려주는 프로퍼티
    /// </summary>
    public bool ProduceCheck
    {
        get { return produceCheck; }
        set { produceCheck = value; }
    }
    /// <summary>
    /// 레이스의 참가하는 동물의 수를 알려주는 프로퍼티
    /// </summary>
    public int AnimalCount
    {
        get { return animalCount; }
        set
        {
            animalCount = value;
        }
    }

    protected override void Initialize()
    {
        //Debug.Log($"{ProduceCheck}");
        //for (int i = 0; i < AnimalCount; i++)
        //{
        //    Debug.Log($"{AnimalNumbers[i]}");
        //}

        for (int i = 0; i < MAXTICKETCOUNT; i++)
        {
            TicketDatas[i] = new TicketData();
        }
      

        gamePlayer = FindObjectOfType<Player>();
    }


}
