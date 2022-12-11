using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour
{
    //티켓 버튼용 스크립트

    WaringMassege waring; //에러메시지 변수

    BetUI betUI; //버튼을 눌렀을때 열리는 변수
    string errorMassage = "아직 레이스가 생성되지 않았습니다!\n" + "생성버튼을 눌러 레이스를 생성하세요!"; //에러메시지
    private void Awake()
    {
        Button myButton = GetComponent<Button>();

        waring = FindObjectOfType<WaringMassege>();
        betUI = FindObjectOfType<BetUI>();
       

        myButton.onClick.AddListener(BetOpen);
    }

    void BetOpen()
    {

        if (GameManager.Instance.ProduceCheck) // 레이스가 생성되있으면 오픈
        {
            betUI.Open();

        }
        else // 레이스가 생성되있지 않으면 에러메시지 오픈
        {

            waring.Open();
            waring.TextChange(errorMassage);
        }

    }


}
