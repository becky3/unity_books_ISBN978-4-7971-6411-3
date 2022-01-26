using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using State = PlayerController.GameState;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public Button restartButton;
    public Button nextButton;

    Image titleImage;

    public GameObject timeBar;
    public TMP_Text timeText;
    TimeController timeCnt;

    public TMP_Text scoreText;
    public static int totalScore;
    public int stageScore = 0;

    public AudioClip meGameOver;
    public AudioClip meGameClear;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0f)
            {
                timeBar.SetActive(false);
            }
        }

        UpdateScore();
    }


    // Update is called once per frame
    void Update()
    {
        switch (PlayerController.gameState)
        {
            case State.gameClear:

                mainImage.SetActive(true);
                panel.SetActive(true);

                restartButton.interactable = false;
                mainImage.GetComponent<Image>().sprite = gameClearSpr;
                PlayerController.gameState = State.gameEnd;

                if (timeCnt != null)
                {
                    timeCnt.isTimeOver = true;

                    var time = (int)timeCnt.displayTime;
                    totalScore += time * 10;
                }

                totalScore += stageScore;
                stageScore = 0;
                UpdateScore();

                ChangeSound(meGameClear);

                break;


            case State.gameOver:

                mainImage.SetActive(true);
                panel.SetActive(true);

                nextButton.interactable = false;
                mainImage.GetComponent<Image>().sprite = gameOverSpr;
                PlayerController.gameState = State.gameEnd;

                if (timeCnt != null)
                {
                    timeCnt.isTimeOver = true;
                }

                ChangeSound(meGameOver);

                break;

            case State.playing:

                var player = GameObject.FindGameObjectWithTag("Player");
                var playerCnt = player.GetComponent<PlayerController>();

                if (timeCnt != null && timeCnt.gameTime > 0f)
                {
                    var time = (int)timeCnt.displayTime;

                    Debug.Log("displayTime:" + timeCnt.displayTime);
                    Debug.Log("time:" + time);
                    timeText.text = time.ToString();

                    if (time == 0)
                    {
                        playerCnt.GameOver();
                    }
                }

                if (playerCnt.score != 0)
                {
                    stageScore += playerCnt.score;
                    playerCnt.score = 0;
                    UpdateScore();
                }

                break;

        }
    }

    private void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    private void UpdateScore()
    {
        var score = stageScore + totalScore;
        scoreText.text = score.ToString();
    }

    private void ChangeSound(AudioClip clip)
    {
        var soundPlayer = GetComponent<AudioSource>();
        if (soundPlayer == null)
        {
            return;
        }

        soundPlayer.Stop();
        soundPlayer.PlayOneShot(clip);
    }

}
