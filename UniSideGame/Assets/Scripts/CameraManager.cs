using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public GameObject subScreen;

    public bool isForceScrollX = false;
    public bool isForceScrollY = false;

    public float forceScrollSpeedX = 0.5f;
    public float forceScrollSpeedY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        var x = player.transform.position.x;
        var y = player.transform.position.y;
        var z = transform.position.z;

        if (isForceScrollX)
        {
            x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
        }
        if (isForceScrollY)
        {
            y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
        }

        if (x < leftLimit)
        {
            x = leftLimit;
        }

        if (x > rightLimit)
        {
            x = rightLimit;
        }

        if (y < bottomLimit)
        {
            y = bottomLimit;
        }

        if (y > topLimit)
        {
            y = topLimit;
        }

        transform.position = new Vector3(x, y, z);

        if (subScreen != null)
        {
            y = subScreen.transform.position.y;
            z = subScreen.transform.position.z;

            subScreen.transform.position = new Vector3(x / 2.0f, y, z);
        }

    }

}
