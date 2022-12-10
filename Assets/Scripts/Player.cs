using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int money=0;

    public int Money
    {
        get { return money; }
        set { money = value;
            onChangeMoney?.Invoke();
        }
    }


    public System.Action onChangeMoney { get; set; }
}
