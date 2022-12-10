using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMoneyUI : MonoBehaviour
{
    TextMeshProUGUI moneyUI;
    GameObject backGround;

    WaringMassege waring;

    string error = "0원일때만 돈을 받을 수 있습니다\n";

    private void Awake()
    {
        Button plusButton=transform.Find("PlusButton").GetComponent<Button>();
        backGround = transform.Find("BackGround").gameObject;
        moneyUI=backGround.GetComponentInChildren<TextMeshProUGUI>();
        waring=FindObjectOfType<WaringMassege>();
       

        plusButton.onClick.AddListener(PlayerMoneyUp);
    }

    private void Start()
    {
        GameManager.Instance.GamePlayer.onChangeMoney += ChangeMoney;
        if (GameManager.Instance.GamePlayer!=null)
        {
            moneyUI.text = string.Format("{0:#,0}", GameManager.Instance.GamePlayer.Money);
        }
    }


    void PlayerMoneyUp()
    {
        if(GameManager.Instance.GamePlayer!=null)
        {
            if(GameManager.Instance.GamePlayer.Money==0)
            {
                GameManager.Instance.GamePlayer.Money+=1000;
            }else
            {
                waring.Open();
                waring.TextChange(error);
            }
        }
    }

    void ChangeMoney()
    {
        moneyUI.text = string.Format("{0:#,0}", GameManager.Instance.GamePlayer.Money);
    }

}
