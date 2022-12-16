using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacingStartButton : MonoBehaviour
{
    //레이스시작버튼 스크립트

    WaringMassege waring;
    string errorMassage = "아직 레이스가 생성되지 않았습니다!\n" + "생성버튼을 눌러 레이스를 생성하세요!";
    private void Awake()
    {
        Button myButton=GetComponent<Button>();
        waring=FindObjectOfType<WaringMassege>();

        myButton.onClick.AddListener(RacingStart);
    }

    

    void RacingStart()
    {
        ClickSound.Instance.ClickPlay();
        if (GameManager.Instance.ProduceCheck)
        {
            //레이스가 생성되있으면 Race씬으로 넘어간다.
            SceneManager.LoadScene((int)StageEnum.Race);
        }
        else
        {
            //레이스 생성이 안되있으면 waring창을 띄운다
            waring.Open();
            waring.TextChange(errorMassage);
        }

    }

    
}
