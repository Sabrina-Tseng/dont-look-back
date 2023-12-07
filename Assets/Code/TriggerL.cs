using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerL : MonoBehaviour
{
    public Transform playerPos;
    public GameObject player;

    public bool doorOpened = false;

    public float triggerDistance = 2f;
    private bool _kicking;

    public Transform door;
    public float doorMoveDistance = 10f;

    void Start()
    {

    }

    void Update()
    {

            float playerDistance = Vector3.Distance(this.transform.position, playerPos.transform.position);
            
            if (playerDistance < triggerDistance)
            {
                _kicking = player.GetComponent<PlayerMove>().kick2;

                if (_kicking)
                {
                    StartCoroutine(Open());
                }
            }

    }

    IEnumerator Open()
    {
        print("dooropen");
        door.transform.position = new Vector2(door.position.x, door.position.y + doorMoveDistance * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
