using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
    BuyUI buyUI;
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        buyUI = GetComponentInChildren<BuyUI>();

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(Close);

        Button buyButton = transform.Find("Buy").GetComponent<Button>();
        buyButton.onClick.AddListener(BuyOpen);

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

    void BuyOpen()
    {
        buyUI.Open();
    }
}
