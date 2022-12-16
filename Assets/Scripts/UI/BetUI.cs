using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
    //티켓을 눌르면 열리는 스크립트 구매와 확인버튼이 할당한다.

    BuyUI buyUI; // 구매버튼 변수
    RectTransform rect;

    TicketInfo ticketInfo; //확인버튼 변수

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
        ClickSound.Instance.ClickPlay();
        rect.anchoredPosition = Vector2.zero;
        gameObject.SetActive(true);
    }

    void Close()
    {
        ClickSound.Instance.ClickPlay();
        gameObject.SetActive(false);
    }

    void BuyOpen()
    {
        ClickSound.Instance.ClickPlay();
        if (GameManager.Instance.TicketCount < GameManager.MAXTICKETCOUNT) //티켓은 10개 까지만 살수있다.
        {
            buyUI.Open();
        }else // 11개를 살려고 하면 에러메시지
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
