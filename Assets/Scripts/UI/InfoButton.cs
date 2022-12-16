using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    //정보버튼 스크립트

    WaringMassege waring;
    AnimalInfo animalInfo;
    string errorMassage = "아직 레이스가 생성되지 않았습니다!\n" + "생성버튼을 눌러 레이스를 생성하세요!";
    private void Awake()
    {
        Button myButton = GetComponent<Button>();
        waring = FindObjectOfType<WaringMassege>();
        animalInfo = FindObjectOfType<AnimalInfo>();

        myButton.onClick.AddListener(InfoOpen);
    }


    /// <summary>
    /// 레이스가 생성되었으면 인포창열고 아니면 경고창을 연다
    /// </summary>
    void InfoOpen()
    {
        ClickSound.Instance.ClickPlay();
        if (GameManager.Instance.ProduceCheck)
        {
            animalInfo.Open();
            
        }
        else
        {
            waring.Open();
            waring.TextChange(errorMassage);
        }

    }

}
