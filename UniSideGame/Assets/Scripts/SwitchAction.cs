using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public MovingBlock targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false;

    private SpriteRenderer spriteRenderer
    {
        get
        {
            return GetComponent<SpriteRenderer>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = on ? imageOn : imageOff;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag != "Player")
        {
            return;
        }

        on = !on;
        spriteRenderer.sprite = on ? imageOn : imageOff;
        targetMoveBlock.SetCanMove(on);


    }
}
