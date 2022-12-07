using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RacingProduce : MonoBehaviour
{
   

     
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
