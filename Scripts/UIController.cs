using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] float scoreMultiplier = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI frontFlipText;
    [SerializeField] TextMeshProUGUI backFlipText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI finalFrontText;
    [SerializeField] TextMeshProUGUI finalBackText;
    [SerializeField] VariableJoystick horizontalJoyStick;
    [SerializeField] VariableJoystick verticalJoyStick;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject text;
    [SerializeField] GameObject pause;

    [HideInInspector] public bool isLeftTurnPressed = false;
    [HideInInspector] public bool isRightTurnPressed = false;
    [HideInInspector] public bool isBoostUpPressed = false;
    [HideInInspector] public bool isBoostDownPressed = false;
    [HideInInspector] public bool isJumpPressed = false;

    [HideInInspector] public float score;

    [HideInInspector] public int frontFlipCount;
    [HideInInspector] public int backFlipCount;

    private void Start()
    {
        DeviceVersion();
    }

    private void Update()
    {
        GameLoop();
    }

    private void DeviceVersion()
    {

#if UNITY_STANDALONE_WIN
            horizontalJoyStick.gameObject.SetActive(false);
            verticalJoyStick.gameObject.SetActive(false);
#endif
    }

    private void GameLoop()
    {
        if (PlayerController.instance.CanMove())
        {
            ScoreDetector();
            FrontAndBackFlip();
            HorizontalJoyStickCheck();
            VerticalJoyStickCheck();
            UpdateHighScore();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void UpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (highScore < (int)score)
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
        }
        highScoreText.text = "HighScore: " + highScore.ToString();
        currentScoreText.text = "Current Score: " + ((int)score).ToString();
    }

    private void FrontAndBackFlip()
    {
        frontFlipText.text = frontFlipCount.ToString() + " :FrontFlip";
        backFlipText.text = backFlipCount.ToString() + " :BackFlip";

        finalFrontText.text ="FrontFlip: " + frontFlipCount.ToString();
        finalBackText.text = backFlipCount.ToString() + " :BackFlip";
    }

    private void  ScoreDetector()
    {
        if (PlayerController.instance.GetAirTime() >= 2.5f)
        {
            score += Time.deltaTime * scoreMultiplier * 7.5f;
        }
        else
        {
            score += Time.deltaTime * scoreMultiplier;
        }

        scoreText.text = "Score: " + ((int)score).ToString();
        finalScoreText.text = "Final Score: " + ((int)score).ToString();
    }

    private void HorizontalJoyStickCheck()
    {
        if (horizontalJoyStick.Horizontal > 0)
        {
            isRightTurnPressed = true;
            isLeftTurnPressed = false;
        }
        else if (horizontalJoyStick.Horizontal < 0)
        {

            isRightTurnPressed = false;
            isLeftTurnPressed = true;
        }
        else
        {

            isRightTurnPressed = false;
            isLeftTurnPressed = false;
        }
    }
    private void VerticalJoyStickCheck()
    {
        if (verticalJoyStick.Vertical > 0)
        {
            isBoostUpPressed = true;
            isBoostDownPressed = false;
        }
        else if (verticalJoyStick.Vertical < 0)
        {

            isBoostUpPressed = false;
            isBoostDownPressed = true;
        }
        else
        {

            isBoostUpPressed = false;
            isBoostDownPressed = false;
        }
    }
}
