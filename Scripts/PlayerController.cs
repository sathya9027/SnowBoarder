using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmmount = 1f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float lowSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    float airTime;
    float frontFlipTorque;
    bool canMove = true;
    bool canJump = false;
    bool playerOnAir = false;
    float backFlipTorque;
    Rigidbody2D rb2D;
    SurfaceEffector2D sF2D;
    UIController uiController;
    Animator anim;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sF2D = FindObjectOfType <SurfaceEffector2D>();
        uiController = FindObjectOfType<UIController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateGameLoop();
    }

    private void UpdateGameLoop()
    {
        if (canMove)
        {
#if UNITY_ANDROID
            CheckAndroid();
#endif
#if UNITY_STANDALONE_WIN
            CheckWindowStandalone();
#endif
            CheckPlayerOnAir();
            CheckFrontFlip();
            CheckBackFlip();
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        if (canJump)
        {
            JumpPlayer();
        }
    }

    private void ResetAnimatorBool()
    {
        anim.SetBool("Flip", false);
        anim.SetBool("Grab", false);
        anim.SetBool("Nose", false);
        anim.SetBool("Grind", false);
        anim.SetBool("Roll", false);
        anim.SetBool("Push", false);
    }

    private void CheckAndroid()
    {
        //Respond To Boost
        if (uiController.isBoostUpPressed)
        {
            sF2D.speed = boostSpeed;
            ResetAnimatorBool();
            anim.SetBool("Roll", true);
        }
        else if (uiController.isBoostDownPressed)
        {
            sF2D.speed = lowSpeed;
            ResetAnimatorBool();
            anim.SetBool("Push", true);
        }
        else
        {
            sF2D.speed = baseSpeed;
        }
        
        //Player Rotation
        if (uiController.isRightTurnPressed)
        {
            rb2D.AddTorque(-torqueAmmount * Time.deltaTime);
            frontFlipTorque += torqueAmmount * Time.deltaTime;
            ResetAnimatorBool();
            anim.SetBool("Nose", true);
        }
        else
        {
            frontFlipTorque = 0;
        }


        if (uiController.isLeftTurnPressed)
        {
            rb2D.AddTorque(torqueAmmount * Time.deltaTime);
            backFlipTorque += torqueAmmount * Time.deltaTime;
            ResetAnimatorBool();
            anim.SetBool("Grind", true);
        }
        else
        {
            backFlipTorque = 0;
        }
    }

    private void CheckWindowStandalone()
    {
        //Respond To Boost
        if (Input.GetKey(KeyCode.W))
        {
            sF2D.speed = boostSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            sF2D.speed = lowSpeed;
        }
        else
        {
            sF2D.speed = baseSpeed;
        }

        //Player Rotation
        if (Input.GetKey(KeyCode.D))
        {
            rb2D.AddTorque(-torqueAmmount * Time.deltaTime);
            frontFlipTorque += torqueAmmount * Time.deltaTime;
        }
        else
        {
            frontFlipTorque = 0;
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb2D.AddTorque(torqueAmmount * Time.deltaTime);
            backFlipTorque += torqueAmmount * Time.deltaTime;
        }
        else
        {
            backFlipTorque = 0;
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    private void JumpPlayer()
    {
        if (uiController.isJumpPressed || Input.GetKey(KeyCode.Space))
        {
            rb2D.AddForce(Vector2.up * jumpSpeed);
        }
    }

    public void DisableControlls()
    {
        canMove = false;
    }

    public void EnableControlls()
    {
        canMove = true;
    }

    private void CheckPlayerOnAir()
    {
        if (playerOnAir)
        {
            airTime += Time.deltaTime;
        }
    }

    private void CheckFrontFlip()
    {
        if (frontFlipTorque >= 2160)
        {
            FindObjectOfType<UIController>().frontFlipCount++;
            frontFlipTorque = 0;
        }
    }

    private void CheckBackFlip()
    {
        if (backFlipTorque >= 2160)
        {
            FindObjectOfType<UIController>().backFlipCount++;
            backFlipTorque = 0;
        }
    }

    public float GetAirTime()
    {
        return airTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            playerOnAir = false;
        }
        backFlipTorque = 0;
        frontFlipTorque = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
            playerOnAir = true;
            airTime = 0;
        }
    }
}
