using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUI : MonoBehaviour
{
    //티켓 구매를 결정하는 스크립트

    bool[] seungsigCheck; //어떤승식을 체크했는지 확인하는 변수
    GameObject seungsigParent; //승식버튼을 가지고있는 부모오브젝트
    Button[] seungsigButton; // 승식버튼 모아둔 변수

    //에러메시지용 변수ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    WaringMassege waringMassege;
    string error01 = "승식이 선택되지 않았습니다.\n" +
        "승식을 선택해주세요";
    string error02 = "동물이 선택되지 않았습니다.\n" +
        "동물을 선택해주세요";
    string error03 = "존재하지 않는 동물을 선택하였습니다.\n";
    string error04 = "같은 동물은 선택할수 없습니다.\n";
    string error05 = "돈이 부족합니다\n";
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    GameObject animalSelect; // 동물선택 버튼최상이 부모오브젝트

    GameObject firstParent; // 동물선택 첫번째줄 부모오브젝트
    GameObject secondParent; // 동물선택 두번째줄 부모오브젝트
    GameObject thirdParent; // 동물선택 세번째줄 부모오브젝트
    Button[] firstButton; //동물선택 번호를 모아둔 변수
    Button[] secondButton; //동물선택 번호를 모아둔 변수
    Button[] thirdButton; //동물선택 번호를 모아둔 변수

    int firstAnimal = 0; //선택한 동물확인용 변수
    int secondAnimal = 0;
    int thirdAnimal = 0;

    GameObject playerMoney; // 플레이어가 소지한 돈을 확인할 변수의 부모오브젝트
    TextMeshProUGUI moneyUI; // 플레이어 돈표시용 변수

    GameObject playerInputMoney; //플레이어가 입력한 돈을 표시할 변수의 부모오브젝트
    TMP_InputField inputMoney; // 플레이어가 돈을 입력하는 인풋필드

    TicketType ticketType=TicketType.None; //현재 플레이어가 선택한 승식을 저장할 변수
    private void Awake()
    {
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        Button ok = transform.Find("OK_Button").GetComponent<Button>();
        waringMassege = FindObjectOfType<WaringMassege>();
        Seungsig();
        AnimalSelectNumbers();
        playerMoney = transform.Find("PlayerMoney").gameObject;
        moneyUI = playerMoney.transform.Find("BackGround").transform.GetComponentInChildren<TextMeshProUGUI>();
        playerInputMoney = transform.Find("PlayerInputMoney").gameObject;
        inputMoney = playerInputMoney.GetComponentInChildren<TMP_InputField>();

        ok.onClick.AddListener(OkButton);
        cancel.onClick.AddListener(Close);

        seungsigCheck = new bool[8] { true, false, false, false, false, false, false, false };
    }

    /// <summary>
    /// 동물번호 선택 버튼할당 함수
    /// </summary>
    private void AnimalSelectNumbers()
    {
        animalSelect = transform.Find("AnimalSelect").gameObject;

        firstParent = animalSelect.transform.GetChild(0).gameObject;
        secondParent = animalSelect.transform.GetChild(1).gameObject;
        thirdParent = animalSelect.transform.GetChild(2).gameObject;
        firstButton = firstParent.GetComponentsInChildren<Button>();
        secondButton = secondParent.GetComponentsInChildren<Button>();
        thirdButton = thirdParent.GetComponentsInChildren<Button>();
        firstButton[0].onClick.AddListener(First1Button);
        firstButton[1].onClick.AddListener(First2Button);
        firstButton[2].onClick.AddListener(First3Button);
        firstButton[3].onClick.AddListener(First4Button);
        firstButton[4].onClick.AddListener(First5Button);
        firstButton[5].onClick.AddListener(First6Button);
        firstButton[6].onClick.AddListener(First7Button);
        firstButton[7].onClick.AddListener(First8Button);
        firstButton[8].onClick.AddListener(First9Button);
        firstButton[9].onClick.AddListener(First10Button);

        secondButton[0].onClick.AddListener(Second1Button);
        secondButton[1].onClick.AddListener(Second2Button);
        secondButton[2].onClick.AddListener(Second3Button);
        secondButton[3].onClick.AddListener(Second4Button);
        secondButton[4].onClick.AddListener(Second5Button);
        secondButton[5].onClick.AddListener(Second6Button);
        secondButton[6].onClick.AddListener(Second7Button);
        secondButton[7].onClick.AddListener(Second8Button);
        secondButton[8].onClick.AddListener(Second9Button);
        secondButton[9].onClick.AddListener(Second10Button);

        thirdButton[0].onClick.AddListener(Third1Button);
        thirdButton[1].onClick.AddListener(Third2Button);
        thirdButton[2].onClick.AddListener(Third3Button);
        thirdButton[3].onClick.AddListener(Third4Button);
        thirdButton[4].onClick.AddListener(Third5Button);
        thirdButton[5].onClick.AddListener(Third6Button);
        thirdButton[6].onClick.AddListener(Third7Button);
        thirdButton[7].onClick.AddListener(Third8Button);
        thirdButton[8].onClick.AddListener(Third9Button);
        thirdButton[9].onClick.AddListener(Third10Button);
    }

    void Start()
    {
        GameManager.Instance.GamePlayer.onChangeMoney += ChangeMoney; //델리게이트로 플레이어 돈이 변화할때마다 변경
        if (GameManager.Instance.GamePlayer != null)
        {
            moneyUI.text = string.Format("{0:#,0}", GameManager.Instance.GamePlayer.Money); //999,999이런식으로 단위수마다 ,를 찍히게 한다.
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 현재가지고 있는 돈표시 변경
    /// </summary>
    void ChangeMoney()
    {
        moneyUI.text = string.Format("{0:#,0}", GameManager.Instance.GamePlayer.Money);
    }

    /// <summary>
    /// 승식관련 버튼을 찾고 할당해주는 함수
    /// </summary>
    private void Seungsig()
    {
        seungsigParent = transform.Find("Seungsig").gameObject;
        seungsigButton = seungsigParent.GetComponentsInChildren<Button>();
        seungsigButton[(int)TicketType.Danseung - 1].onClick.AddListener(DanseungOnClick);
        seungsigButton[(int)TicketType.Yeonseung - 1].onClick.AddListener(YeonseungOnClick);
        seungsigButton[(int)TicketType.Bogseung - 1].onClick.AddListener(BogseungOnClick);
        seungsigButton[(int)TicketType.Ssangseung - 1].onClick.AddListener(SsangseungOnClick);
        seungsigButton[(int)TicketType.Bogyeonseung - 1].onClick.AddListener(BogyeonseungOnClick);
        seungsigButton[(int)TicketType.Sambogseung - 1].onClick.AddListener(SambogseungOnClick);
        seungsigButton[(int)TicketType.Samssangseung - 1].onClick.AddListener(SamssangseungOnClick);
    }



    public void Open()
    {
        gameObject.SetActive(true);
        
    }
    /// <summary>
    /// ok버튼에 할당할 변수 현재 돈,승식,선택한 동물에따라 실패하면 에러가나오면 성공하면 끝이난다.
    /// </summary>
    void OkButton()
    {
        int valueMoney = int.Parse(inputMoney.text);

        if (valueMoney == 0 || GameManager.Instance.GamePlayer.Money < valueMoney)
        {
            waringMassege.Open();
            waringMassege.TextChange(error05);
        }
        else
        {

            if (seungsigCheck[0])
            {
                waringMassege.Open();
                waringMassege.TextChange(error01);
            }
            else
            {
                if (seungsigCheck[1] || seungsigCheck[2])
                {
                    if (firstAnimal == 0)
                    {
                        waringMassege.Open();
                        waringMassege.TextChange(error02);
                    }
                    else
                    {
                        if (firstAnimal > GameManager.Instance.AnimalCount)
                        {
                            waringMassege.Open();
                            waringMassege.TextChange(error03);
                        }
                        else
                        {
                            TicketBuy(GameManager.Instance.TicketCount, ticketType, valueMoney, firstAnimal);
                            GameManager.Instance.TicketCount++;
                            Closeinitialize();
                            GameManager.Instance.GamePlayer.Money -= valueMoney;
                            

                            gameObject.SetActive(false);
                        }
                    }
                }
                else if (seungsigCheck[3] || seungsigCheck[4] || seungsigCheck[5])
                {
                    if (firstAnimal == 0 || secondAnimal == 0)
                    {
                        waringMassege.Open();
                        waringMassege.TextChange(error02);
                    }
                    else
                    {
                        if (firstAnimal > GameManager.Instance.AnimalCount || secondAnimal > GameManager.Instance.AnimalCount)
                        {
                            waringMassege.Open();
                            waringMassege.TextChange(error03);
                        }
                        else
                        {
                            if (firstAnimal == secondAnimal)
                            {
                                waringMassege.Open();
                                waringMassege.TextChange(error04);
                            }
                            else
                            {
                                TicketBuy(GameManager.Instance.TicketCount, ticketType, valueMoney, firstAnimal, secondAnimal);
                                Closeinitialize();
                                GameManager.Instance.GamePlayer.Money -= valueMoney;

                                
                                GameManager.Instance.TicketCount++;

                                gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    if (firstAnimal == 0 || secondAnimal == 0 || thirdAnimal == 0)
                    {
                        waringMassege.Open();
                        waringMassege.TextChange(error02);
                    }
                    else
                    {
                        if (firstAnimal > GameManager.Instance.AnimalCount || secondAnimal > GameManager.Instance.AnimalCount || thirdAnimal > GameManager.Instance.AnimalCount)
                        {
                            waringMassege.Open();
                            waringMassege.TextChange(error03);
                        }
                        else
                        {
                            if (firstAnimal == secondAnimal || secondAnimal == thirdAnimal || firstAnimal == thirdAnimal)
                            {
                                waringMassege.Open();
                                waringMassege.TextChange(error04);
                            }
                            else
                            {
                                TicketBuy(GameManager.Instance.TicketCount, ticketType, valueMoney, firstAnimal, secondAnimal, thirdAnimal);
                                Closeinitialize();
                                GameManager.Instance.GamePlayer.Money -= valueMoney;

                                
                                GameManager.Instance.TicketCount++;
                                gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 구매에 성공했을 경우 GameManager에 데이터를 넘기는 함수
    /// </summary>
    /// <param name="idx">TicketDatas의 인덱스 0~9가 되어야한다.</param>
    /// <param name="ticketType">어떤종료의 티켓인지?</param>
    /// <param name="vM">얼마를 배팅했는지?</param>
    /// <param name="fA">첫번째 동물</param>
    /// <param name="sA">두번째 동물</param>
    /// <param name="tA">세번째 동물</param>
    private void TicketBuy(int idx, TicketType ticketType, int vM, int fA,int sA=0,int tA=0)
    {
        GameManager.Instance.TicketDatas[idx].ticketType = ticketType;
        GameManager.Instance.TicketDatas[idx].first = fA;
        GameManager.Instance.TicketDatas[idx].second = sA;
        GameManager.Instance.TicketDatas[idx].third = tA;
        GameManager.Instance.TicketDatas[idx].moneyAmount = vM;
    }

    void Close()
    {
        Closeinitialize();

        gameObject.SetActive(false);

    }

    /// <summary>
    /// 창이 닫힐때 초기화해줘야할것을 모아둔 함수
    /// </summary>
    private void Closeinitialize()
    {
        seungsigCheck[0] = true;
        for (int i = 1; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }

        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstAnimal = 0;

        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondAnimal = 0;

        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdAnimal = 0;
        ticketType = TicketType.None;
        inputMoney.text = "0";
    }

    //승식버튼함수ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    void DanseungOnClick()
    {
        for(int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for(int i=0; i<seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Danseung;
        seungsigCheck[(int)TicketType.Danseung] = true;
        seungsigButton[(int)TicketType.Danseung-1].interactable = false;

    }
    void YeonseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Yeonseung;
        seungsigCheck[(int)TicketType.Yeonseung] = true;
        seungsigButton[(int)TicketType.Yeonseung - 1].interactable = false;

    }
    void BogseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Bogseung;
        seungsigCheck[(int)TicketType.Bogseung] = true;
        seungsigButton[(int)TicketType.Bogseung - 1].interactable = false;

    }
    void SsangseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Ssangseung;
        seungsigCheck[(int)TicketType.Ssangseung] = true;
        seungsigButton[(int)TicketType.Ssangseung - 1].interactable = false;

    }
    void BogyeonseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Bogyeonseung;
        seungsigCheck[(int)TicketType.Bogyeonseung] = true;
        seungsigButton[(int)TicketType.Bogyeonseung - 1].interactable = false;

    }
    void SambogseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Sambogseung;
        seungsigCheck[(int)TicketType.Sambogseung] = true;
        seungsigButton[(int)TicketType.Sambogseung - 1].interactable = false;

    }
    void SamssangseungOnClick()
    {
        for (int i = 0; i < seungsigButton.Length; i++)
        {
            seungsigButton[i].interactable = true;
        }
        for (int i = 0; i < seungsigCheck.Length; i++)
        {
            seungsigCheck[i] = false;
        }
        ticketType = TicketType.Samssangseung;
        seungsigCheck[(int)TicketType.Samssangseung] = true;
        seungsigButton[(int)TicketType.Samssangseung - 1].interactable = false;

    }

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
   

    //첫번째 라인 버튼에 할당하는 함수ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    void First1Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[0].interactable = false;
        firstAnimal = 1;
        
    }
    void First2Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[1].interactable = false;
        firstAnimal = 2;

    }
    void First3Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[2].interactable = false;
        firstAnimal = 3;

    }
    void First4Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[3].interactable = false;
        firstAnimal = 4;

    }
    void First5Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[4].interactable = false;
        firstAnimal = 5;

    }
    void First6Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[5].interactable = false;
        firstAnimal = 6;

    }
    void First7Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[6].interactable = false;
        firstAnimal = 7;

    }
    void First8Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[7].interactable = false;
        firstAnimal = 8;

    }
    void First9Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[8].interactable = false;
        firstAnimal = 9;

    }
    void First10Button()
    {
        for (int i = 0; i < firstButton.Length; i++)
        {
            firstButton[i].interactable = true;
        }
        firstButton[9].interactable = false;
        firstAnimal = 10;

    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    //두번째 라인 버튼할당 함수ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    void Second1Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[0].interactable = false;
        secondAnimal = 1;

    }
    void Second2Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[1].interactable = false;
        secondAnimal = 2;

    }
    void Second3Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[2].interactable = false;
        secondAnimal = 3;

    }
    void Second4Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[3].interactable = false;
        secondAnimal = 4;

    }
    void Second5Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[4].interactable = false;
        secondAnimal = 5;

    }
    void Second6Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[5].interactable = false;
        secondAnimal = 6;

    }
    void Second7Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[6].interactable = false;
        secondAnimal = 7;

    }
    void Second8Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[7].interactable = false;
        secondAnimal = 8;

    }
    void Second9Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[8].interactable = false;
        secondAnimal = 9;

    }
    void Second10Button()
    {
        for (int i = 0; i < secondButton.Length; i++)
        {
            secondButton[i].interactable = true;
        }
        secondButton[9].interactable = false;
        secondAnimal = 10;

    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    //세번째 번호라인 할당함수ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    void Third1Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[0].interactable = false;
        thirdAnimal = 1;

    }
    void Third2Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[1].interactable = false;
        thirdAnimal = 2;

    }
    void Third3Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[2].interactable = false;
        thirdAnimal = 3;

    }
    void Third4Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[3].interactable = false;
        thirdAnimal = 4;

    }
    void Third5Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[4].interactable = false;
        thirdAnimal = 5;

    }
    void Third6Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[5].interactable = false;
        thirdAnimal = 6;

    }
    void Third7Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[6].interactable = false;
        thirdAnimal = 7;

    }
    void Third8Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[7].interactable = false;
        thirdAnimal = 8;

    }
    void Third9Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[8].interactable = false;
        thirdAnimal = 9;

    }
    void Third10Button()
    {
        for (int i = 0; i < thirdButton.Length; i++)
        {
            thirdButton[i].interactable = true;
        }
        thirdButton[9].interactable = false;
        thirdAnimal = 10;

    }

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
}
