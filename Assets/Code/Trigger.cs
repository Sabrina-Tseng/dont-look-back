using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool rightKick = false;
    public bool leftKick = false;
    private bool _right = false;
    private bool _left = false;

    public Transform playerPos;
    private float triggerDistance = 1f;
    public GameObject player;
    private bool _kicking;

    public bool hasWire = false;
    public GameObject wire;

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
                if (rightKick && leftKick)
                {
                    _right = player.GetComponent<PlayerMove>().kick1;
                    _left = player.GetComponent<PlayerMove>().kick2;

                    if ( _right && _left)
                    {
                        _kicking = true;
                    }
                }
                else if (rightKick)
                {
                    _kicking = player.GetComponent<PlayerMove>().kick1;
                }
                else if (leftKick)
                {
                    _kicking = player.GetComponent<PlayerMove>().kick2;
                }

                if (_kicking)
                {
                    StartCoroutine(Open());
                }
            }

    }

    IEnumerator Open()
    {
        //print("dooropen");
        door.transform.position = new Vector2(door.position.x, door.position.y + doorMoveDistance * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        
        if (hasWire)
        {
            print("color");
            var wireRenderer = wire.GetComponent<SpriteRenderer>();
            wireRenderer.color = Color.white;
        }

        Destroy(this);
    }
}
