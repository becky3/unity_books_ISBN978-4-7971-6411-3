using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public int score = 0;

    public enum GameState
    {
        playing,
        gameOver,
        gameClear,
        gameEnd,
    }

    new Rigidbody2D rigidbody2D;
    float axisH = 0.0f;
    public float speed = 3.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    Animator animator;
    string stopAnime = "PlayerStop";
    string moveAnime = "PlayerMove";
    string jumpAnime = "PlayerJump";
    string goalAnime = "PlayerGoal";
    string deadAnime = "PlayerOver";

    string nowAnime = "";
    string oldAnime = "";

    bool isMoving = false;

    public static GameState gameState = GameState.playing;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = nowAnime;

        gameState = GameState.playing;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameState.playing)
        {
            return;
        }
        if (!isMoving)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }

        if (axisH > 0f)
        {
            transform.localScale = new Vector2(
                1, 1
            );
        }
        else if (axisH < 0f)
        {
            transform.localScale = new Vector2(
                -1, 1
            );
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {

        if (gameState != GameState.playing)
        {
            return;
        }

        onGround = Physics2D.Linecast(
            transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer
        );

        if (onGround || axisH != 0)
        {
            rigidbody2D.velocity = new Vector2(
                speed * axisH,
                rigidbody2D.velocity.y
            );
        }

        if (onGround && goJump)
        {
            var jumpPw = new Vector2(0, jump);
            rigidbody2D.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Goal":
                Goal();
                break;

            case "Dead":
                GameOver();
                break;

            case "ScoreItem":

                var item = other.gameObject.GetComponent<ItemData>();
                score = item.value;

                Destroy(other.gameObject);

                break;

        }

    }



    public void Jump()
    {
        goJump = true;

    }

    private void Goal()
    {
        animator.Play(goalAnime);

        gameState = GameState.gameClear;
        GameStop();
    }

    private void GameStop()
    {
        var rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = Vector2.zero;
    }

    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = GameState.gameOver;
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false;
        rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }

    public void SetAxis(float h, float v)
    {
        axisH = h;

        isMoving = axisH != 0;
    }
}
