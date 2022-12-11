using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultTicketInfo : MonoBehaviour
{
    //레이스 결과에서 티켓결과를 보여줄 스크립트
    //InfoTickect과 기능이 같지만 혼란을 피하기위해 따로 만들었다

    TextMeshProUGUI seungsig;
    TextMeshProUGUI number;
    TextMeshProUGUI value;

    private void Awake()
    {
        seungsig=transform.Find("Seungsig").GetComponent<TextMeshProUGUI>();
        number = transform.Find("Number").GetComponent<TextMeshProUGUI>();
        value = transform.Find("Value").GetComponent<TextMeshProUGUI>();
    }

    public void infoChange(TicketType ticketType, int first, int second, int third, int money)
    {

        if (ticketType == TicketType.Danseung)
        {
            seungsig.text = "단승";
        }
        if (ticketType == TicketType.Yeonseung)
        {
            seungsig.text = "연승";
        }
        if (ticketType == TicketType.Bogseung)
        {
            seungsig.text = "복승";
        }
        if (ticketType == TicketType.Ssangseung)
        {
            seungsig.text = "쌍승";
        }
        if (ticketType == TicketType.Bogyeonseung)
        {
            seungsig.text = "복연승";
        }
        if (ticketType == TicketType.Sambogseung)
        {
            seungsig.text = "삼복승";
        }
        if (ticketType == TicketType.Samssangseung)
        {
            seungsig.text = "삼쌍승";
        }



        string numbers = first.ToString();
        if (second != 0)
        {
            numbers += $"/{second}";
        }
        if (third != 0)
        {
            numbers += $"/{third}";
        }

        number.text = numbers;

        value.text = string.Format("{0:#,0}", money);
    }

}
