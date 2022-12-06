using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RacingProduce : MonoBehaviour
{
    // Start is called before the first frame update

    AnimalAI[] animals; // 동물들의 정보를 저장할 변수

    Transform[] startPosition;

    Button produceButton;
    
    void Start()
    {
        animals = FindObjectsOfType<AnimalAI>(); // AnimalAI타입을 전부 찾는다.
        startPosition = GameObject.FindWithTag("StartPoint").GetComponentsInChildren<Transform>();
        produceButton=GetComponent<Button>();
        produceButton.onClick.AddListener(Shuffle);
    }

    void Shuffle()
    {
        for(int i=animals.Length-1; i>-1; i--)
        {
            int randIndex = Random.Range(0, i);
            (animals[randIndex], animals[i]) = (animals[i], animals[randIndex]);

        }

        for(int i=0; i<animals.Length; i++)
        {
            animals[i].transform.position = startPosition[i + 1].position;
        }


    }
}
