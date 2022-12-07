using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ProduceSpliterUI : MonoBehaviour
{
    TMP_InputField inputField;
    int animalCount = 6;
    //Transform[] startPosition;
    //RankManager rankManager;

    RectTransform rect;
    int AnimalCount
    {
        get { return animalCount; }
        set { animalCount = Mathf.Clamp(value,6,10); 
        
        }
    }
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        inputField = GetComponentInChildren<TMP_InputField>();
        //rankManager = FindObjectOfType<RankManager>();
        //startPosition = GameObject.FindWithTag("StartPoint").GetComponentsInChildren<Transform>();
        Button increase = transform.Find("IncreaseButton").GetComponent<Button>();
        increase.onClick.AddListener(OnIncrease);

        Button decrease = transform.Find("DecreaseButton").GetComponent<Button>();
        decrease.onClick.AddListener(OnDecrease);


        Button ok = transform.Find("OK_Button").GetComponent<Button>();
        ok.onClick.AddListener(AnimalProduce);

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(Close);
    }

    void Start()
    {
        

        gameObject.SetActive(false);
 
    }
   

    public void Open()
    {
        gameObject.SetActive(true);
        rect.anchoredPosition = new Vector2(100, -50);
        inputField.text = AnimalCount.ToString();
    }

    void OnIncrease()
    {
        AnimalCount++;
        inputField.text = AnimalCount.ToString();
    }
    void OnDecrease()
    {
        AnimalCount--;
        inputField.text = AnimalCount.ToString();

    }

    void Close()
    {
        gameObject.SetActive(false);
    }

    void AnimalProduce()
    {


        for (int i = GameManager.Instance.AnimalNumbers.Length - 1; i > -1; i--)
        {
            int randIndex = Random.Range(0, i);
            (GameManager.Instance.AnimalNumbers[randIndex], GameManager.Instance.AnimalNumbers[i]) = (GameManager.Instance.AnimalNumbers[i], GameManager.Instance.AnimalNumbers[randIndex]);

        }
        GameManager.Instance.ProduceCheck = true;
        GameManager.Instance.AnimalCount = AnimalCount;

        gameObject.SetActive(false);

        //for (int i = 0; i < rankManager.Animals.Length; i++)
        //{
        //    rankManager.Animals[i].gameObject.SetActive(false);
        //}

        //for (int i = 0; i < AnimalCount; i++)
        //{
        //    rankManager.Animals[animalNumber[i]].gameObject.SetActive(true);
        //    rankManager.Animals[animalNumber[i]].transform.position = startPosition[i + 1].position;
        //}


    }
}
