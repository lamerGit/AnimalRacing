using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene창에 이동경로를 보여줄 스크립트
/// </summary>
public class PathScript : MonoBehaviour
{
    public GameObject waypointController; //경로가 전부 들어있는 GameObject
    List<Transform> path; //waypointController에서 받아온 경로를 저장할 리스트
    Color rayColor = Color.white; // 경로를 표시해줄 색

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        //waypointController의 모든 자식의 Transform을 받아온다
        Transform[] potentialWaypoints = waypointController.GetComponentsInChildren<Transform>();

        path = new List<Transform>();


        for (int i = 0; i < potentialWaypoints.Length; i++)
        {
            if (potentialWaypoints[i] != waypointController.transform)
            {
                path.Add(potentialWaypoints[i]);
            }
        }


        for (int i = 0; i < path.Count; i++)
        {
            Vector3 pos = path[i].position;

            if (i > 0)
            {
                Vector3 prev = path[i - 1].position;
                Gizmos.DrawLine(prev, pos); //prev 에서 pos까지 선을 그어준다
            }

            Gizmos.DrawWireSphere(pos, 3); // 원을 그려서 WayPoint의 위치를 좀더 잘보이게 한다.
        }

    }
}
