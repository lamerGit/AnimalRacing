using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;

    RankManager race;

    private void Awake()
    {
        race = FindObjectOfType<RankManager>();
    }

    private void Start()
    {
        if (race != null)
        {

            race.swapGate += (t) =>
            {
                target = t;
            };
        }
    }

    private void LateUpdate()
    {
        
        if (target != null)
        {
            transform.forward = target.forward;
        }
    }
}
