using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Direction
    {
        left,
        right,
    }

    public float speed = 3f;
    public Direction direction = Direction.left;
    public float range = 0f;
    Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == Direction.right)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        defPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (range <= 0f)
        {
            return;
        }

        if (transform.position.x < defPos.x - (range / 2))
        {
            direction = Direction.right;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > defPos.x + (range / 2))
        {
            direction = Direction.left;
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void FixedUpdate()
    {
        var rbody = GetComponent<Rigidbody2D>();

        if (direction == Direction.right)
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (direction == Direction.right)
        {
            direction = Direction.left;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            direction = Direction.right;
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
