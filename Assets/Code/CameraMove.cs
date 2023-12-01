using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 2;
    void Update()
    {
        Vector3 camPos = transform.position;
        camPos.x+= speed * Time.deltaTime;
        transform.position = camPos;
    }
}
