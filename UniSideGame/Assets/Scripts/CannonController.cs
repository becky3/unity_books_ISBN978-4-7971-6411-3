using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;
    public float delayTime = 3f;
    public float fireSpeedX = -4f;
    public float fireSpeedY = 0f;
    public float length = 8f;

    GameObject player;
    GameObject gateObj;
    float passedTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        var tr = transform.Find("gate");
        gateObj = tr.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;

        if (!CheckLength(player.transform.position))
        {
            return;
        }

        if (passedTimes <= delayTime)
        {
            return;
        }

        passedTimes = 0;

        var pos = new Vector3(
            gateObj.transform.position.x,
            gateObj.transform.position.y,
            transform.position.z
        );

        var obj = Instantiate(
            objPrefab,
            pos,
            Quaternion.identity
        );

        var rbody = obj.GetComponent<Rigidbody2D>();
        var v = new Vector2(fireSpeedX, fireSpeedY);
        rbody.AddForce(v, ForceMode2D.Impulse);
    }

    bool CheckLength(Vector2 targetPos)
    {
        var d = Vector2.Distance(transform.position, targetPos);
        return length >= d;
    }
}
