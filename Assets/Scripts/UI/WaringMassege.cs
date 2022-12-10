using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaringMassege : MonoBehaviour
{
    //레이스가 생성되지 않은 상태에서 시작이나 정보창을 눌렀을때 생기는 오브젝트 스크립트
    TextMeshProUGUI textMeshProUGUI;
    RectTransform rect;
    private void Awake()
    {
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        rect=GetComponent<RectTransform>();

        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        cancel.onClick.AddListener(Close);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        rect.anchoredPosition = Vector2.zero;
        gameObject.SetActive(true);
    }
    void Close()
    {
        gameObject.SetActive(false);
    }

    public void TextChange(string s)
    {
        textMeshProUGUI.text = s;
    }
}
