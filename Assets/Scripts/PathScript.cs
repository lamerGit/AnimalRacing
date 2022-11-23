using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sceneâ�� �̵���θ� ������ ��ũ��Ʈ
/// </summary>
public class PathScript : MonoBehaviour
{
    public GameObject waypointController; //��ΰ� ���� ����ִ� GameObject
    List<Transform> path; //waypointController���� �޾ƿ� ��θ� ������ ����Ʈ
    Color rayColor = Color.white; // ��θ� ǥ������ ��

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        //waypointController�� ��� �ڽ��� Transform�� �޾ƿ´�
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
                Gizmos.DrawLine(prev, pos); //prev ���� pos���� ���� �׾��ش�
            }

            Gizmos.DrawWireSphere(pos, 3); // ���� �׷��� WayPoint�� ��ġ�� ���� �ߺ��̰� �Ѵ�.
        }

    }
}
