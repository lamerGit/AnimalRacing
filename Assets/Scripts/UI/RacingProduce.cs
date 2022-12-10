using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RacingProduce : MonoBehaviour
{
   //생성버튼 스크립트

     
    ProduceSpliterUI produceSpliterUI;

    private void Awake()
    {
        produceSpliterUI = FindObjectOfType<ProduceSpliterUI>();
        Button produceButton = GetComponent<Button>();
        produceButton.onClick.AddListener(OpenSpliter);
    }


    void OpenSpliter()
    {

        produceSpliterUI.Open();


    }


}
