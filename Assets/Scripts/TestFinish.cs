using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestFinish : MonoBehaviour
{
    //동물이 결승점을 통과했을때 사용되는 스크립트

    public FollowCamera camera5;

    int ranking = 0;

    WaitForSeconds returnTime = new WaitForSeconds(3.0f);

    /// <summary>
    /// ranking이 동물의 수와 같아진다면 티켓의 성공여부를 변경하고
    /// 3초뒤 로비로 가는 코루틴을 작동하는 프로퍼티
    /// </summary>
    int Ranking
    {
        get { return ranking; }
        set
        {
            ranking = value;
            if (ranking == GameManager.Instance.AnimalCount)
            {
                
                StartCoroutine(ReturnRobby());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            camera5.finish = true;
            //GameManager.Instance.AnimalRanking[Ranking] = other.GetComponent<AnimalAI>().AnimalNumber;
            //Debug.Log($"{ranking}등 {other.name}");
            Ranking++;
        }
    }

    IEnumerator ReturnRobby()
    {
        yield return returnTime;
        SceneManager.LoadScene(2);
    }
}
