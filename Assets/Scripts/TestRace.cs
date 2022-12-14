using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRace : MonoBehaviour
{
    //테스트용 스크립트
    //

    AnimalAI[] animals; // 동물들의 정보를 저장할 변수

    public FollowCamera[] cameras;

    FollowCamera mainCamera;

    int count = 49;

    SecondCameraOut secondCamera;
    bool secondOut = false;

    Transform[] startPosition;

    Door gateDoor;

    GameObject animalGroup;

    AudioSource audioSource;


    FollowCamera MainCamera
    {
        get { return mainCamera; }
        set { mainCamera = value;
            testDel?.Invoke(mainCamera.transform);
        
        
        }
    }

    public System.Action<Transform> testDel { get; set; }


    /// <summary>
    /// 다른곳에서 동물변수를 사용할수있게 해주는 프로퍼티
    /// </summary>
    public AnimalAI[] Animals
    {
        get { return animals; }
        set { animals = value; }
    }

    private void Awake()
    {
        animalGroup = GameObject.FindGameObjectWithTag("AnimalGroup");//부모오브젝트를 찾고
        animals = animalGroup.GetComponentsInChildren<AnimalAI>(); // 부모오브젝트에 GetComponents를 해야 순서대로 가져온다
        audioSource = GetComponent<AudioSource>();


        secondCamera = FindObjectOfType<SecondCameraOut>();
        startPosition = GameObject.FindWithTag("StartPoint").GetComponentsInChildren<Transform>();
        gateDoor = FindObjectOfType<Door>();
    }

    private void Start()
    {


        mainCamera = cameras[0];
        CamraSwap();
        StartCoroutine(raceStart());
        //StartCoroutine(sortTime());
        //Initialize();

    }

    /// <summary>
    /// 초기화 함수 모든 동물을 비활성화하고 GameManager의 데이터에 따라 동물을 활성화하고 레이스를 시작한다.  
    /// </summary>
    void Initialize()
    {
        for (int i = 0; i < Animals.Length; i++)
        {
            Animals[i].gameObject.SetActive(false);
        }

        if (GameManager.Instance != null)
        {

            //GameManger의 데이터에 따라 동물들 배치, 번호할당
            for (int i = 0; i < GameManager.Instance.AnimalCount; i++)
            {
                int index = GameManager.Instance.AnimalNumbers[i];
                Animals[index].gameObject.SetActive(true);
                Animals[index].AnimalNumber = i + 1;
                Animals[index].transform.position = startPosition[i + 1].position;
            }
            GameManager.Instance.AnimalRanking = new int[GameManager.Instance.AnimalCount];

            GameManager.Instance.BackGroundAudioSource.mute = true;
            StartCoroutine(raceStart());
        }
        else
        {
            Debug.Log("GameManger가 없습니다. 로비씬부터 시작하세요");
        }
    }

    /// <summary>
    /// 레이스 활성 코룬틴
    /// </summary>
    /// <returns></returns>
    IEnumerator raceStart()
    {
        yield return new WaitForSeconds(3.0f);
        gateDoor.DoorOpen();
        for (int i = 0; i < Animals.Length; i++)
        {
            //audioSource.Play();

            Animals[i].RaceStarted = true;
        }
    }


    /// <summary>
    /// 1등 동물에 따라 카메라를 변경하는 스크립트
    /// </summary>
    void CamraSwap()
    {
        if (animals[0].CurrentWayPoint == 5)
        {
            if (!secondOut)
            {
                secondOut = true;
                StartCoroutine(secondCamera.CameraOut());
            }
        }


        if (animals[0].CurrentWayPoint == 8)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(false);
            MainCamera = cameras[2];


        }


        if (animals[0].CurrentWayPoint == 18)
        {
            cameras[2].gameObject.SetActive(false);
            MainCamera = cameras[3];
        }

        if (animals[0].CurrentWayPoint == 28)
        {
            cameras[3].gameObject.SetActive(false);
            MainCamera = cameras[4];
        }

        if (animals[0].CurrentWayPoint == count)
        {
            MainCamera.offset.x += 1;
            MainCamera.offset.z += 1;
            MainCamera.offset.y += 1;
            if (count < 59)
            {
                count++;
            }
        }



        if (animals[0].CurrentWayPoint == 59)
        {
            MainCamera.lookAtMode = true;
        }



        MainCamera.target = animals[0].transform;
        cameras[1].target = animals[0].transform;


    }
    private void FixedUpdate()
    {
        MergeSort(animals, 0, animals.Length - 1);
        CamraSwap();

    }


    /// <summary>
    /// 병합정렬 구현
    /// </summary>
    /// <param name="array">동물들이 담긴변수</param>
    /// <param name="beginIndex">시작인덱스</param>
    /// <param name="midIndex">중간인덱스</param>
    /// <param name="endIndex">마지막인덴스</param>
    void Merge(AnimalAI[] array, int beginIndex, int midIndex, int endIndex)
    {
        //array배열을 반반 나눈다.
        AnimalAI[] lowHalf = new AnimalAI[midIndex + 1];
        AnimalAI[] highHalf = new AnimalAI[endIndex - midIndex];

        int k = beginIndex;
        int index_L = 0;
        int index_H = 0;

        //반반나눈 배열에 array에 있는 정보 할당
        for (int i = 0; k <= midIndex; i++, k++)
        {
            lowHalf[i] = array[k];
        }
        for (int j = 0; k <= endIndex; j++, k++)
        {
            highHalf[j] = array[k];
        }
        k = beginIndex;

        //어느 한쪽의 인덱스가 끝까지 가면 끝
        while (index_L < lowHalf.Length && index_H < highHalf.Length)
        {
            //이동할 지점이 큰수록 앞에있는 동물이다
            if (lowHalf[index_L].CurrentWayPoint > highHalf[index_H].CurrentWayPoint)
            {

                array[k] = lowHalf[index_L];
                index_L++;


            }
            else if (lowHalf[index_L].CurrentWayPoint < highHalf[index_H].CurrentWayPoint)
            {

                array[k] = highHalf[index_H];
                index_H++;

            }
            else
            {
                if ((lowHalf[index_L].CurrentWaypointDistance < highHalf[index_H].CurrentWaypointDistance) && (lowHalf[index_L].CurrentWayPoint == highHalf[index_H].CurrentWayPoint))
                {
                    array[k] = lowHalf[index_L];
                    index_L++;
                }
                else
                {
                    array[k] = highHalf[index_H];
                    index_H++;
                }
            }

            k++;
        }
        //남은 짜투리 할당
        while (index_L < lowHalf.Length)
        {
            array[k] = lowHalf[index_L];
            index_L++;
            k++;
        }
        while (index_H < highHalf.Length)
        {
            array[k] = highHalf[index_H];
            index_H++;
            k++;
        }

    }

    /// <summary>
    /// 분할정복해서 병합정렬을 사용하는 함수
    /// </summary>
    /// <param name="array">동물들이 담긴 배열</param>
    /// <param name="beginIndex">시작인덱스</param>
    /// <param name="endIndex">마지막인덱스</param>
    void MergeSort(AnimalAI[] array, int beginIndex, int endIndex)
    {
        //시작지점이 끝나는 지점보다 작을때만
        if (beginIndex < endIndex)
        {
            int midindex = (beginIndex + endIndex) / 2;
            MergeSort(array, beginIndex, midindex);
            MergeSort(array, midindex + 1, array.Length - 1);
            Merge(array, 0, midindex, array.Length - 1);
        }
    }

}
