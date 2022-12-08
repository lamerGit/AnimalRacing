using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    //정보버튼 스크립트

    WaringMassege waring;
    AnimalInfo animalInfo;
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
        
        if (GameManager.Instance.ProduceCheck)
        {
            animalInfo.Open();
            
        }
        else
        {

            waring.gameObject.SetActive(true);
            waring.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

    }

}
