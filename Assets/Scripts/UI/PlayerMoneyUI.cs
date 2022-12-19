using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMoneyUI : MonoBehaviour
{
    //왼쪽위에 플레이어의 돈을 보여주는 스크립트

    TextMeshProUGUI moneyUI;
    GameObject backGround;

    WaringMassege waring;

    string error = "소지금이 0원이고 소지한 티켓이 없어야 돈을 받을 수 있습니다\n";

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

    /// <summary>
    /// 플레이어가 0원밖에 없을때 +버튼을 통해 돈을지급받을수 있게 해주는 스크립트
    /// </summary>

    void PlayerMoneyUp()
    {
        if(GameManager.Instance.GamePlayer!=null)
        {
            if(GameManager.Instance.GamePlayer.Money==0 && GameManager.Instance.TicketCount==0)
            {
                GameManager.Instance.GamePlayer.Money+=1000;
            }else
            {
                waring.Open();
                waring.TextChange(error);
            }
        }
    }

    /// <summary>
    /// 델리게이트용 함수
    /// </summary>
    void ChangeMoney()
    {
        moneyUI.text = string.Format("{0:#,0}", GameManager.Instance.GamePlayer.Money);
    }

}
