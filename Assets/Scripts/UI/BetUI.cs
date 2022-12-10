using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
    BuyUI buyUI;
    RectTransform rect;

    TicketInfo ticketInfo;

    WaringMassege waringMassege;
    string error = "더이상 티켓을 구매할수 없습니다.\n";
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        buyUI = GetComponentInChildren<BuyUI>();
        ticketInfo = GetComponentInChildren<TicketInfo>();

        waringMassege=FindObjectOfType<WaringMassege>();

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(Close);

        Button buyButton = transform.Find("Buy").GetComponent<Button>();
        buyButton.onClick.AddListener(BuyOpen);

        Button confirmButton = transform.Find("Confirm").GetComponent<Button>();
        confirmButton.onClick.AddListener(TicketInfoOpen);

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
        if (GameManager.Instance.TicketCount < GameManager.MAXTICKETCOUNT)
        {
            buyUI.Open();
        }else
        {
            waringMassege.Open();
            waringMassege.TextChange(error);
        }
    }

    void TicketInfoOpen()
    {
        ticketInfo.Open();
    }
}
