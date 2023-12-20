using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

    public Transform player;

    public float minXClamp;
    public float maxXClamp;
    public float minYClamp;
    public float maxYClamp;

    // Start is called before the first frame update
    private void LateUpdate()
    {
        Vector3 cameraPos;

        cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(player.transform.position.x, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);
        transform.position = cameraPos;

    }
}
