using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    int[] animalNumbers=new int[10] {0,1,2,3,4,5,6,7,8,9}; //레이스에 참가하는 동물들
    bool produceCheck = false; //레이스가 생성됬는지 확인하는 변수
    int animalCount = 6;

    public int[] AnimalNumbers
    {
        get { return animalNumbers; }
        set { animalNumbers = value; }
    }

    public bool ProduceCheck
    {
        get { return produceCheck; }
        set { produceCheck = value; }
    }
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
        
    }

    
}
