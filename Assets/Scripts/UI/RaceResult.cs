using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaceResult : MonoBehaviour
{
    RectTransform rect;

    ResultAnimalInfo[] resultAnimalInfo;
    ResultTicketInfo[] resultTicketInfo;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();


        Button button = transform.Find("Cancel_Button").GetComponent<Button>();
        button.onClick.AddListener(Close);

        GameObject animalInfoParent = transform.Find("Animals").gameObject;
        resultAnimalInfo = animalInfoParent.GetComponentsInChildren<ResultAnimalInfo>();

        GameObject ticketInfoParent = transform.Find("Tickets").gameObject;
        resultTicketInfo= ticketInfoParent.GetComponentsInChildren<ResultTicketInfo>();


    }

    private void Start()
    {
        Close();
        if(GameManager.Instance.ProduceCheck)
        {
            Open();
        }

    }
    public void Open()
    {
        gameObject.SetActive(true);

        for(int i=0; i<resultAnimalInfo.Length; i++)
        {
            resultAnimalInfo[i].gameObject.SetActive(false);
        }

        for(int i=0; i<resultTicketInfo.Length; i++)
        {
            resultTicketInfo[i].gameObject.SetActive(false);
        }


        for(int i=0; i<GameManager.Instance.AnimalCount; i++)
        {
            resultAnimalInfo[i].gameObject.SetActive(true);
            int n = GameManager.Instance.AnimalRanking[i];
            string an = GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[n-1]].name;
            resultAnimalInfo[i].InfoChange(n, an);

        }


        for(int i=0; i<GameManager.Instance.TicketCount; i++)
        {
            resultTicketInfo[i].gameObject.SetActive(true);
            TicketType tp = GameManager.Instance.TicketDatas[i].ticketType;
            int f = GameManager.Instance.TicketDatas[i].first;
            int s = GameManager.Instance.TicketDatas[i].second;
            int t = GameManager.Instance.TicketDatas[i].third;
            int value = 0;

            if (GameManager.Instance.TicketDatas[i].ticketState==TicketState.success)
            {
                value = MoneyCalculation(tp, f, s, t, GameManager.Instance.TicketDatas[i].moneyAmount);
                GameManager.Instance.GamePlayer.Money += value;
            }

            resultTicketInfo[i].infoChange(tp, f, s, t, value);
            

        }

        GameManager.Instance.ProduceCheck = false;
        GameManager.Instance.AnimalCount = 0;
        GameManager.Instance.TicketCount = 0;

        for(int i=0; i < GameManager.Instance.TicketDatas.Length; i++)
        {
            GameManager.Instance.TicketDatas[i].TicketInicialize();
        }

        rect.anchoredPosition = Vector2.zero;
    }

    int MoneyCalculation(TicketType ticketType,int f,int s,int t,int money)
    {
        int result = 0;


        if (ticketType == TicketType.Danseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            result =money*firstMagni*2;
        }
        if (ticketType == TicketType.Yeonseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            result = money * firstMagni;
        }
        if (ticketType == TicketType.Bogseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            int secondMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[s - 1]].allocation;
            result = money * (firstMagni+secondMagni);
        }
        if (ticketType == TicketType.Ssangseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            int secondMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[s - 1]].allocation;
            result = money * ((firstMagni * secondMagni));
        }
        if (ticketType == TicketType.Bogyeonseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            int secondMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[s - 1]].allocation;
            result = money * ((firstMagni + secondMagni));
        }
        if (ticketType == TicketType.Sambogseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            int secondMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[s - 1]].allocation;
            int thirdMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[t - 1]].allocation;
            result = money * ((firstMagni + secondMagni+thirdMagni));
        }
        if (ticketType == TicketType.Samssangseung)
        {
            int firstMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[f - 1]].allocation;
            int secondMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[s - 1]].allocation;
            int thirdMagni = (int)GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[t - 1]].allocation;
            result = money * ((firstMagni * secondMagni * thirdMagni));
        }

        return result;
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
