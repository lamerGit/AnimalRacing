using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacingStartButton : MonoBehaviour
{
    

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
            //Debug.Log($"{GameManager.Instance.ProduceCheck}");
            //for(int i = 0; i < GameManager.Instance.AnimalCount; i++)
            //{
            //    Debug.Log($"{GameManager.Instance.AnimalNumbers[i]}");
            //}
            SceneManager.LoadScene((int)StageEnum.Race);
        }
        else
        {
            waring.gameObject.SetActive(true);
        }

    }

    
}
