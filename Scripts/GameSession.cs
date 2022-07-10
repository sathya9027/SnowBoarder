using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Cinemachine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loadingIcon;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Button continueButton;
    [SerializeField] AudioSource music;

    private void Update()
    {
        UpdateGameLoop();
    }

    private void UpdateGameLoop()
    {
        DeviceVersion();
    }

    private void DeviceVersion()
    {
#if UNITY_STANDALONE_WIN
        pauseButton.SetActive(false);
        FindObjectOfType<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10;
#endif
#if UNITY_ANDROID
        pauseButton.SetActive(true);
        FindObjectOfType<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 8;
#endif
    }

    public void MainMenu()
    {
        //SceneManager.LoadScene(0);
        //Time.timeScale = 1;
        music.Stop();
        StartCoroutine(LoadMainMenu());
    }

    public void Resume()
    {
        Time.timeScale = 1;
        music.Play();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        music.Pause();
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        AdManager.instance.ShowAd(FindObjectOfType<CrashDetector>());

        continueButton.interactable = false;
    }

    public IEnumerator LoadMainMenu()
    {
        loadingScreen.SetActive(true);

        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(0);

        aSyncLoad.allowSceneActivation = false;

        while (!aSyncLoad.isDone)
        {
            loadingSlider.value = aSyncLoad.progress;
            if (aSyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Press To Continue.....";
                loadingIcon.SetActive(false);
                loadingSlider.gameObject.SetActive(false);

                if (Input.anyKeyDown)
                {
                    aSyncLoad.allowSceneActivation = true;

                    Time.timeScale = 1;
                }
            }

            yield return null;
        }
    }
}
