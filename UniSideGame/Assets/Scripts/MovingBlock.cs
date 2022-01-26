using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0f;
    public float moveY = 0f;

    public float times = 0f;
    public float weight = 0f;
    public bool isMoveWhenOn = false;
    public bool isCanMove = true;

    float perDx;
    float perDy;
    Vector3 defPos;
    bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        var timeStep = Time.deltaTime;
        perDx = moveX / (1f / timeStep * times);
        perDy = moveY / (1f / timeStep * times);

        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isCanMove)
        {
            return;
        }

        var x = transform.position.x;
        var y = transform.position.y;
        var endX = false;
        var endY = false;

        if (isReverse)
        {
            if ((perDx >= 0f && x <= defPos.x) || (perDx < 0f && x >= defPos.x))
            {
                endX = true;
            }

            if ((perDy >= 0f && y <= defPos.y) || (perDy < 0f && y >= defPos.y))
            {
                endY = true;
            }

            transform.Translate(new Vector3(-perDx, -perDy, defPos.z));

        }
        else
        {
            if ((perDx >= 0f && x >= defPos.x + moveX) || (perDx < 0f && x <= defPos.x + moveX))
            {
                endX = true;
            }

            if ((perDy >= 0f && y >= defPos.y + moveY) || (perDy < 0f && y <= defPos.y + moveY))
            {
                endY = true;
            }

            transform.Translate(new Vector3(perDx, perDy, defPos.z));
        }

        if (endX && endY)
        {
            if (isReverse)
            {
                transform.position = defPos;
            }

            isReverse = !isReverse;
            isCanMove = false;
            if (!isMoveWhenOn)
            {
                Invoke("Move", weight);
            }

        }


    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        other.transform.SetParent(transform);
        if (isMoveWhenOn)
        {
            isCanMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        other.transform.SetParent(null);
    }
}