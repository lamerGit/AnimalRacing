using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    //동물이 결승점을 통과했을때 사용되는 스크립트

    public FollowCamera camera5;

    int ranking = 0;

    WaitForSeconds returnTime = new WaitForSeconds(3.0f);

    int Ranking
    {
        get { return ranking; }
        set
        {
            ranking = value;
            if(ranking==GameManager.Instance.AnimalCount)
            {
                GameManager.Instance.TicketCheck();
                Debug.Log("계산완료");

                for(int i=0; i<GameManager.Instance.TicketCount; i++)
                {
                    Debug.Log(GameManager.Instance.TicketDatas[i].ticketState);
                }

                StartCoroutine( ReturnRobby());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Animal"))
        {
            camera5.finish = true;
            GameManager.Instance.AnimalRanking[Ranking] = other.GetComponent<AnimalAI>().AnimalNumber;
            Debug.Log($"{ranking}등 {other.name}");
            Ranking++;
        }
    }

    IEnumerator ReturnRobby()
    {
        yield return returnTime;
        SceneManager.LoadScene((int)StageEnum.Lobby);
    }
}
