using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaceResult : MonoBehaviour
{
    //경기가 끝났을때 보여주는 스크립트

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
        //씬이 시작되고 Start함수가 실행됬을때 GameManger가 레이스를 생성한 상태이면 레이스를 시작하고
        //돌아온것이라고 인지하고 레이스결과창을 띄운다.
        if(GameManager.Instance.ProduceCheck)
        {
            Open();
        }

    }
    public void Open()
    {
        gameObject.SetActive(true);

        //동물순위표시
        for(int i=0; i<resultAnimalInfo.Length; i++)
        {
            resultAnimalInfo[i].gameObject.SetActive(false);
        }

        //티켓표시
        for(int i=0; i<resultTicketInfo.Length; i++)
        {
            resultTicketInfo[i].gameObject.SetActive(false);
        }


        for(int i=0; i<GameManager.Instance.AnimalCount; i++)
        {
            resultAnimalInfo[i].gameObject.SetActive(true);
            int n = GameManager.Instance.AnimalRanking[i];
            //동물의 순위에서 번호를 얻고 그 번호로 AnimalNumbers에서 동물의 번호를 받은 것을 animalDatas에 넣으면 어떤 동물인지 확인가능
            string an = GameManager.Instance.animalDatas[GameManager.Instance.AnimalNumbers[n-1]].name;
            resultAnimalInfo[i].InfoChange(n, an);

        }

        //티켓 체크
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
        //레이스 정보 초기화
        GameManager.Instance.ProduceCheck = false;
        GameManager.Instance.AnimalCount = 0;
        GameManager.Instance.TicketCount = 0;
        GameManager.Instance.BackGroundAudioSource.mute = false;

        for (int i=0; i < GameManager.Instance.TicketDatas.Length; i++)
        {
            GameManager.Instance.TicketDatas[i].TicketInicialize();
        }

        rect.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// 배팅에 성공하면 그것에 맞는 배당을 리턴하는 함수
    /// </summary>
    /// <param name="ticketType">배팅타입</param>
    /// <param name="f">첫번째 동물</param>
    /// <param name="s">두번째 동물</param>
    /// <param name="t">세번째 동물</param>
    /// <param name="money">배팅한 금액</param>
    /// <returns>계산된 금액</returns>
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
