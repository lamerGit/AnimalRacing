using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RankManager : MonoBehaviour
{
    //현재 동물들의 순위확인용 스크립트

    AnimalAI[] animalRank; // 동물들의 정보를 저장할 변수

    public FollowCamera[] cameras;

    FollowCamera mainCamera;

    int count = 49;

    SecondCameraOut secondCamera;
    bool secondOut = false;
    private void Start()
    {
        animalRank=FindObjectsOfType<AnimalAI>(); // AnimalAI타입을 전부 찾는다.
        secondCamera=FindObjectOfType<SecondCameraOut>();

        mainCamera = cameras[0];
        CamraSwap();
        //StartCoroutine(sortTime());
        
    }

    

    void CamraSwap()
    {
        if (animalRank[0].CurrentWayPoint == 5)
        {
            if (!secondOut)
            {
                secondOut = true;
                StartCoroutine(secondCamera.CameraOut());
            }
        }


        if (animalRank[0].CurrentWayPoint == 8)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(false);
            mainCamera = cameras[2];

            
        }


        if (animalRank[0].CurrentWayPoint == 18)
        {
            cameras[2].gameObject.SetActive(false);
            mainCamera = cameras[3];
        }

        if (animalRank[0].CurrentWayPoint == 28)
        {
            cameras[3].gameObject.SetActive(false);
            mainCamera = cameras[4];
        }

        if (animalRank[0].CurrentWayPoint == count)
        {
            mainCamera.offset.z += 1;
            if (count < 59)
            {
                count++;
            }
        }



        if (animalRank[0].CurrentWayPoint == 59)
        {
            mainCamera.lookAtMode=true;
        }

        

        mainCamera.target = animalRank[0].transform;
        cameras[1].target = animalRank[0].transform;

       
    }
    private void FixedUpdate()
    {
        MergeSort(animalRank, 0, animalRank.Length - 1);
        CamraSwap();
        
    }

    IEnumerator sortTime()
    {
        while (true)
        {
            MergeSort(animalRank, 0, animalRank.Length - 1);
            CamraSwap();
            yield return new WaitForSeconds(0.1f);
            
        }
    }

    /// <summary>
    /// 병합정렬 구현
    /// </summary>
    /// <param name="array">동물들이 담긴변수</param>
    /// <param name="beginIndex">시작인덱스</param>
    /// <param name="midIndex">중간인덱스</param>
    /// <param name="endIndex">마지막인덴스</param>
    void Merge(AnimalAI[] array, int beginIndex, int midIndex,int endIndex)
    {
        //array배열을 반반 나눈다.
        AnimalAI[] lowHalf = new AnimalAI[midIndex + 1];
        AnimalAI[] highHalf = new AnimalAI[endIndex-midIndex];

        int k = beginIndex;
        int index_L = 0;
        int index_H = 0;
        
        //반반나눈 배열에 array에 있는 정보 할당
        for(int i=0; k<=midIndex; i++,k++)
        {
            lowHalf[i] = array[k];
        }
        for(int j=0; k<=endIndex; j++,k++)
        {
            highHalf[j]= array[k];
        }
        k= beginIndex;

        //어느 한쪽의 인덱스가 끝까지 가면 끝
        while(index_L<lowHalf.Length && index_H<highHalf.Length)
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

            }else
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
        while(index_L<lowHalf.Length)
        {
            array[k] = lowHalf[index_L];
            index_L++;
            k++;
        }
        while(index_H<highHalf.Length)
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
    void MergeSort(AnimalAI[] array,int beginIndex,int endIndex)
    {
        //시작지점이 끝나는 지점보다 작을때만
        if(beginIndex<endIndex)
        {
            int midindex = (beginIndex + endIndex) / 2;
            MergeSort(array, beginIndex, midindex);
            MergeSort(array,midindex+1,array.Length-1);
            Merge(array,0,midindex,array.Length-1);
        }
    }

}
