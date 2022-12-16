using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaringMassege : MonoBehaviour
{
    //오류메시지 전달용 스크립트
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
        ClickSound.Instance.ClickPlay();
        gameObject.SetActive(false);
    }

    public void TextChange(string s)
    {
        textMeshProUGUI.text = s;
    }
}
