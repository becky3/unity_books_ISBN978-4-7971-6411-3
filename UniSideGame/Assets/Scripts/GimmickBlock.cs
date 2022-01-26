using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{

    public float length = 0f;
    public bool isDelete = false;

    bool isFell = false;
    float fadeTime = 0.5f;

    new Rigidbody2D rigidbody2D
    {
        get
        {
            return GetComponent<Rigidbody2D>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        updateBodyType();

        updateColor();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isDelete)
        {
            isFell = true;
        }
    }

    void updateBodyType()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        var d = transform.position.x - player.transform.position.x;

        Debug.Log("d:" + d);

        if (length >= d)
        {

            if (rigidbody2D.bodyType == RigidbodyType2D.Static)
            {
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    void updateColor()
    {

        if (!isFell)
        {
            return;
        }

        fadeTime -= Time.deltaTime;
        var sr = GetComponent<SpriteRenderer>();
        var col = sr.color;
        col.a = fadeTime;
        sr.color = col;

        if (fadeTime <= 0f)
        {
            Destroy(gameObject);
        }

    }

}
