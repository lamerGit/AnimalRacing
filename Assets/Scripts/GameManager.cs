using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class GameManager : Singleton<GameManager>
{
    //게임매니저 스크립트 싱글톤을 상속받는다.


    public int[] animalNumbers = new int[11] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10 }; //레이스에 참가하는 동물들
    bool produceCheck = false; //레이스가 생성됬는지 확인하는 변수
    int animalCount = 6; //레이스에 참가하는 동물의 수

    Player gamePlayer;

    public AnimalData[] animalDatas; // 동물들의 스크립트오브젝트를 받는다.

    int ticketCount = 0; // 현재 가지고 있는 티켓의 수
    TicketData[] ticketDatas = new TicketData[10]; //티켓변수

    public static int MAXTICKETCOUNT = 10; // 티켓을 10개로 제한용 변수

    int[] animalRanking; //동물들이 도착한 순서를 기록해줄 변수

    AudioSource backGroundAudioSource;

    public AudioClip[] audioClips;

    PlayerInput inputActions;
    Option option;

    public AudioMixer audioMixer;
    float musicVolume=0.5f;
    float cfxVolume=0.5f;

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }

    }

    public float CfxVolume
    {
        get { return cfxVolume; }
        set
        {
            cfxVolume = value;
        }
    }

    public AudioSource BackGroundAudioSource
    {
        get { return backGroundAudioSource; }

    }
    public int[] AnimalRanking
    {
        get { return animalRanking; }
        set { animalRanking = value; }
    }

    public int TicketCount
    {
        get { return ticketCount; }
        set { ticketCount = value; }
    }

    public TicketData[] TicketDatas
    {
        get { return ticketDatas; }
        set { ticketDatas = value; }

    }

    public Player GamePlayer
    {
        get { return gamePlayer; }
    }

    /// <summary>
    /// 레이스의 참가하는 동물들의 프로퍼티
    /// </summary>
    public int[] AnimalNumbers
    {
        get { return animalNumbers; }
        set { animalNumbers = value; }
    }

    /// <summary>
    /// 레이스생성 여부를 알려주는 프로퍼티
    /// </summary>
    public bool ProduceCheck
    {
        get { return produceCheck; }
        set { produceCheck = value; }
    }
    /// <summary>
    /// 레이스의 참가하는 동물의 수를 알려주는 프로퍼티
    /// </summary>
    public int AnimalCount
    {
        get { return animalCount; }
        set
        {
            animalCount = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        backGroundAudioSource = GetComponent<AudioSource>();
        
        for (int i = 0; i < MAXTICKETCOUNT; i++)
        {
            TicketDatas[i] = new TicketData();
        }
        inputActions = new();

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.UI.Enable();
        inputActions.UI.Esc.performed += OnEsc;
        inputActions.UI.Enter.performed += OnEnter;
        inputActions.UI.Touch.performed += OnTouch;
    }

    

    protected override void OnDisable()
    {
        base.OnDisable();
        //inputActions.UI.Esc.performed -= OnEsc;
        //inputActions.UI.Disable();
    }
    private void OnTouch(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)StageEnum.Title)
        {
            SceneManager.LoadScene((int)StageEnum.Lobby);
        }

    }
    private void OnEnter(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)StageEnum.Title)
        {
            SceneManager.LoadScene((int)StageEnum.Lobby);
        }

    }
    private void OnEsc(InputAction.CallbackContext obj)
    {
        if (option.isActiveAndEnabled)
        {
            option.Close();
        }
        else
        {
            option.Open();
        }
    }

    

    protected override void Initialize()
    {
        option = FindObjectOfType<Option>();
        gamePlayer = FindObjectOfType<Player>();
        if(SceneManager.GetActiveScene().buildIndex==(int)StageEnum.Title)
        {
            backGroundAudioSource.clip = audioClips[(int)StageEnum.Title];
            backGroundAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == (int)StageEnum.Lobby)
        {
            backGroundAudioSource.clip = audioClips[(int)StageEnum.Lobby];
            backGroundAudioSource.Play();
        }

    }

    /// <summary>
    /// 동물의 랭킹의 따라 티켓의 성공여부를 확인하는 함수
    /// </summary>
    public void TicketCheck()
    {
        for (int i = 0; i < ticketCount; i++)
        {
            if (ticketDatas[i].ticketType==TicketType.Danseung)
            {
                if (ticketDatas[i].first == animalRanking[0])
                {
                    ticketDatas[i].ticketState = TicketState.success;
                }else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }

            if (ticketDatas[i].ticketType == TicketType.Yeonseung)
            {
                if (ticketDatas[i].first == animalRanking[0] || ticketDatas[i].first== animalRanking[1] || ticketDatas[i].first == animalRanking[2] )
                {
                    ticketDatas[i].ticketState = TicketState.success;
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }

            if (ticketDatas[i].ticketType == TicketType.Bogseung)
            {
                if (ticketDatas[i].first == animalRanking[0] || ticketDatas[i].first == animalRanking[1])
                {
                    if (ticketDatas[i].second == animalRanking[0] || ticketDatas[i].second == animalRanking[1])
                    {
                        ticketDatas[i].ticketState = TicketState.success;
                    }else
                    {
                        ticketDatas[i].ticketState = TicketState.failure;
                    }
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }


            if (ticketDatas[i].ticketType == TicketType.Ssangseung)
            {
                if (ticketDatas[i].first == animalRanking[0])
                {
                    if (ticketDatas[i].second == animalRanking[1])
                    {
                        ticketDatas[i].ticketState = TicketState.success;
                    }else
                    {
                        ticketDatas[i].ticketState = TicketState.failure;
                    }
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }


            if (ticketDatas[i].ticketType == TicketType.Bogyeonseung)
            {
                if (ticketDatas[i].first == animalRanking[0] || ticketDatas[i].first == animalRanking[1] || ticketDatas[i].first == animalRanking[2])
                {
                    if (ticketDatas[i].second == animalRanking[0] || ticketDatas[i].second == animalRanking[1] || ticketDatas[i].second == animalRanking[2])
                    {
                        ticketDatas[i].ticketState = TicketState.success;
                    }else
                    {
                        ticketDatas[i].ticketState = TicketState.failure;
                    }
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }


            if (ticketDatas[i].ticketType == TicketType.Sambogseung)
            {
                if (ticketDatas[i].first == animalRanking[0] || ticketDatas[i].first == animalRanking[1] || ticketDatas[i].first == animalRanking[2])
                {
                    if (ticketDatas[i].second == animalRanking[0] || ticketDatas[i].second == animalRanking[1] || ticketDatas[i].second == animalRanking[2])
                    {
                        if (ticketDatas[i].third == animalRanking[0] || ticketDatas[i].third == animalRanking[1] || ticketDatas[i].third == animalRanking[2])
                        {
                            ticketDatas[i].ticketState = TicketState.success;
                        }else
                        {
                            ticketDatas[i].ticketState = TicketState.failure;
                        }

                    }else
                    {
                        ticketDatas[i].ticketState = TicketState.failure;
                    }
                    
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }


            if (ticketDatas[i].ticketType == TicketType.Samssangseung)
            {
                if (ticketDatas[i].first == animalRanking[0])
                {
                    if (ticketDatas[i].second == animalRanking[1])
                    {
                        if (ticketDatas[i].third == animalRanking[2])
                        {
                            ticketDatas[i].ticketState = TicketState.success;

                        }else
                        {
                            ticketDatas[i].ticketState = TicketState.failure;
                        }
                    }else
                    {
                        ticketDatas[i].ticketState = TicketState.failure;
                    }

                    
                }
                else
                {
                    ticketDatas[i].ticketState = TicketState.failure;
                }
            }



        }
    }

}
