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
    private void Awake()
    {
        Button myButton=GetComponent<Button>();
        waring=FindObjectOfType<WaringMassege>();

        myButton.onClick.AddListener(RacingStart);
    }

    

    void RacingStart()
    {
        if (GameManager.Instance.ProduceCheck)
        {
            //레이스가 생성되있으면 Race씬으로 넘어간다.
            SceneManager.LoadScene((int)StageEnum.Race);
        }
        else
        {
            //레이스 생성이 안되있으면 waring창을 띄운다
            waring.gameObject.SetActive(true);
            waring.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

    }

    
}
