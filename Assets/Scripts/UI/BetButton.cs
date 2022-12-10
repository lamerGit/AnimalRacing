using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour
{
    WaringMassege waring;

    BetUI betUI;
    string errorMassage = "아직 레이스가 생성되지 않았습니다!\n" + "생성버튼을 눌러 레이스를 생성하세요!";
    private void Awake()
    {
        Button myButton = GetComponent<Button>();

        waring = FindObjectOfType<WaringMassege>();
        betUI = FindObjectOfType<BetUI>();
       

        myButton.onClick.AddListener(BetOpen);
    }

    void BetOpen()
    {

        if (GameManager.Instance.ProduceCheck)
        {
            betUI.Open();

        }
        else
        {

            waring.Open();
            waring.TextChange(errorMassage);
        }

    }


}
