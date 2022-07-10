using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1.5f;
    [SerializeField] ParticleSystem finishEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            finishEffect.Play();
            Invoke("ReloadScene", sceneLoadDelay);
            GetComponent<AudioSource>().Play();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
