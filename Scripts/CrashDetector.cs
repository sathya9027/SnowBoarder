using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] GameObject player;

    Transform startTransform;
    UIController uiController;
    bool hasCrashed = false;

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            PlayerController.instance.DisableControlls();
            Invoke(nameof(GameOver), sceneLoadDelay);
            crashEffect.Play();
            GetComponent<AudioSource>().Play();
        }
    }

    private void GameOver()
    {
        uiController.gameOver.SetActive(true);
        uiController.text.SetActive(false);
    }

    public void ContinueGame()
    {
        Destroy(player);
        FindObjectOfType<TerrainCreation>().SpawnPlayer();
        uiController.gameOver.SetActive(false);
        uiController.text.SetActive(true);
        hasCrashed = false;
    }
}
