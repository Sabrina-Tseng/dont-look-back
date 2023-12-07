using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Transform playerPos;
    public GameObject player;
    public float triggerDistance = 5f;

    void Start()
    {
        //player = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(this.transform.position, playerPos.transform.position);
        
        if (playerDistance < triggerDistance)
        {
            print("distance"+playerDistance);
            // if (player.kicking)
            // {
            //     print("dooropen");
            // }
        }
    }
}
