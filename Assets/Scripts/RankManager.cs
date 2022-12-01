using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RankManager : MonoBehaviour
{
    AnimalAI[] animalRank;

    private void Start()
    {
        animalRank=FindObjectsOfType<AnimalAI>();
        
  
        
    }

    private void Update()
    {
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Time.timeScale = 1.0f;
            

        }

    }

    private void FixedUpdate()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Time.timeScale = 0.0f;

            MergeSort(animalRank, 0, animalRank.Length - 1);
            for (int i = 0; i < animalRank.Length; i++)
            {
                Debug.Log($"현재 {i+1}등 : {animalRank[i].name} {animalRank[i].CurrentWayPoint} {animalRank[i].CurrentWaypointDistance} {animalRank[i].animalNumber}");
            }
            

        }

    }

    void Merge(AnimalAI[] array, int beginIndex, int midIndex,int endIndex)
    {
        AnimalAI[] lowHalf = new AnimalAI[midIndex + 1];
        AnimalAI[] highHalf = new AnimalAI[endIndex-midIndex];

        int k = beginIndex;
        int index_L = 0;
        int index_H = 0;
        
        for(int i=0; k<=midIndex; i++,k++)
        {
            lowHalf[i] = array[k];
        }
        for(int j=0; k<=endIndex; j++,k++)
        {
            highHalf[j]= array[k];
        }
        k= beginIndex;

        while(index_L<lowHalf.Length && index_H<highHalf.Length)
        {
            if (lowHalf[index_L].CurrentWayPoint > highHalf[index_H].CurrentWayPoint)
            {
                array[k] = lowHalf[index_L];
                index_L++;
            }else
            {
                if (lowHalf[index_L].CurrentWayPoint == highHalf[index_H].CurrentWayPoint)
                {
                    if (lowHalf[index_L].CurrentWaypointDistance < highHalf[index_H].CurrentWaypointDistance)
                    {
                        array[k] = lowHalf[index_L];
                        index_L++;
                    }else
                    {
                        array[k] = highHalf[index_H];
                        index_H++;
                    }

                }
                else
                {
                    array[k] = highHalf[index_H];
                    index_H++;
                }
            }

            k++;
        }

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

    void MergeSort(AnimalAI[] array,int beginIndex,int endIndex)
    {
        if(beginIndex<endIndex)
        {
            int midindex = (beginIndex + endIndex) / 2;
            MergeSort(array, beginIndex, midindex);
            MergeSort(array,midindex+1,array.Length-1);
            Merge(array,0,midindex,array.Length-1);
        }
    }

}
