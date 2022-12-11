using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketInfo : MonoBehaviour
{
    

    GameObject infoParent; // infoTicket을 전부 가지고 있는 부모오브젝트
    InfoTicket[] tickets;

    private void Awake()
    {
        
        infoParent = transform.Find("Tickets").gameObject; //부모오브젝트를 먼저 찾고
        tickets = infoParent.GetComponentsInChildren<InfoTicket>();

        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>(); // 닫기버튼 찾기
        cancel.onClick.AddListener(Close); // 닫기버튼에 Close함수 할당
    }

    private void Start()
    {
        

        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < tickets.Length; i++)
        {
            tickets[i].gameObject.SetActive(false);
        }

        for(int i=0; i<GameManager.Instance.TicketCount; i++)
        {
            tickets[i].gameObject.SetActive(true);
            TicketType t = GameManager.Instance.TicketDatas[i].ticketType;
            int f = GameManager.Instance.TicketDatas[i].first;
            int s = GameManager.Instance.TicketDatas[i].second;
            int third = GameManager.Instance.TicketDatas[i].third;
            int m = GameManager.Instance.TicketDatas[i].moneyAmount;

            tickets[i].infoChange(t,f,s,third,m);


        }

    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
