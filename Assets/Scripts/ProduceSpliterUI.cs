using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ProduceSpliterUI : MonoBehaviour
{
    //생성버튼을 눌렀을때 열리는 스플리터창

    TMP_InputField inputField;
    int animalCount = 6;
    

    RectTransform rect;
    /// <summary>
    /// 동물의 수를 6~10마리로 제한하기위한 프로퍼티
    /// </summary>
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
        Button increase = transform.Find("IncreaseButton").GetComponent<Button>(); //왼쪽버튼
        increase.onClick.AddListener(OnIncrease);

        Button decrease = transform.Find("DecreaseButton").GetComponent<Button>(); // 오른쪽버튼
        decrease.onClick.AddListener(OnDecrease);


        Button ok = transform.Find("OK_Button").GetComponent<Button>(); // ok버튼
        ok.onClick.AddListener(AnimalProduce);

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>(); // x버튼
        cancel.onClick.AddListener(Close);
    }

    void Start()
    {
        

        gameObject.SetActive(false);
 
    }
   
    /// <summary>
    /// 오브젝트를 키면서 위치를 변경하고 인풋필드 갱신
    /// </summary>
    public void Open()
    {
        gameObject.SetActive(true);
        rect.anchoredPosition = new Vector2(100, -50);
        inputField.text = AnimalCount.ToString();
    }

    /// <summary>
    /// AnimalCount증가 및 인풋필드갱신
    /// </summary>
    void OnIncrease()
    {
        AnimalCount++;
        inputField.text = AnimalCount.ToString();
    }
    /// <summary>
    /// AnimalCount감소 및 인풋필드갱신
    /// </summary>
    void OnDecrease()
    {
        AnimalCount--;
        inputField.text = AnimalCount.ToString();

    }

    void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ok를 했을때 GameManger에 AnimalNumbers를 피셔에이츠알고리즘으로 섞고
    /// 레이스생성여부 true
    /// 레이스에 사용될 동물의 수를 갱싱하는 함수
    /// </summary>
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

        

    }
}
