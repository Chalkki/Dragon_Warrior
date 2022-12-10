using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Use LateUpdate to update the camera position
    void LateUpdate()
    {
        if(!player) { return; }
        tempPos = player.position;
        tempPos.z = -10;
        transform.position = tempPos;
    }
}
