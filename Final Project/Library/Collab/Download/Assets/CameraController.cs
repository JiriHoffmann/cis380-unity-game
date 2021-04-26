using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    Vector3 cameraPos;
    Vector3 playerPos;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = mainCamera.transform.position;
        playerPos = player.transform.position;
        float posXDelta = cameraPos.x - playerPos.x;
        float posYDelta = cameraPos.y - playerPos.y;
        Vector3 newPos = cameraPos;
        Debug.Log(playerPos);
        if (posXDelta > 10)
        {
            newPos.x = cameraPos.x + posXDelta - 10;
        }
        else if (posXDelta < -10)
        {
            newPos.x = cameraPos.x + posXDelta + 10;
        }
        if (posYDelta > 10)
        {
            newPos.y = cameraPos.y - (cameraPos.y - playerPos.y);
        }
        newPos.z = -10;

        mainCamera.transform.position = newPos;
    }
}
