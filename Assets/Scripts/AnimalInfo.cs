using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalInfo : MonoBehaviour
{
    //정보창을 눌렀을때 레이스가 생성되있으면 나타는 창

    public AnimalData[] animalDatas; // 동물들의 스크립트오브젝트를 받는다.

    Info[] infos; // 동물의 번호 이름 스킬이름 배당률을 보여주는 오브젝트
    RectTransform rect; 
    GameObject infoParent; // info를 전부 가지고 있는 부모오브젝트
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        infoParent = transform.Find("Animals").gameObject; //부모오브젝트를 먼저 찾고
        infos = infoParent.GetComponentsInChildren<Info>(); // 부모오브젝트에서 받아와야 순서대로 가져올수 있다

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>(); // 닫기버튼 찾기
        cancel.onClick.AddListener(Close); // 닫기버튼에 Close함수 할당
    }

    private void Start()
    {
        Close();
    }

    /// <summary>
    /// 인포창이 열렸을때의 함수
    /// </summary>
    public void Open()
    {
        gameObject.SetActive(true);
        rect.anchoredPosition = Vector2.zero; // 화면중앙으로 이동

        //먼저 모든 인포 오브젝트를 가린다.
        for(int i = 0; i < infos.Length; i++)
        {
            infos[i].gameObject.SetActive(false);
        }

        //레이스에 사용할 동물 만큼 인포를 다시킨다
        for(int i=0; i<GameManager.Instance.AnimalCount; i++)
        {
            infos[i].gameObject.SetActive(true);
            int number = i+1;
            string an = animalDatas[GameManager.Instance.AnimalNumbers[i]].name;
            string sp = animalDatas[GameManager.Instance.AnimalNumbers[i]].special;
            float al = animalDatas[GameManager.Instance.AnimalNumbers[i]].allocation;

            infos[i].InfoChange(number, an, sp, al);
        }


    }
    void Close()
    {
        gameObject.SetActive(false);
    }
}
