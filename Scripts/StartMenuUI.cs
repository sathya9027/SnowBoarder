using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Image image;
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    [SerializeField] GameObject pcInstructuion;
    [SerializeField] GameObject mobileInstructuion;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loadingIcon;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] Slider loadingSlider;
    bool isAudioMuted = false;

    private void Start()
    {
        StartGameLoop();
    }

    private void Update()
    {
        UpdateGameLoop();
    }

    private void UpdateGameLoop()
    {
        DeviceVersion();
    }

    private void StartGameLoop()
    {
        mainMenu.SetActive(true);
        pcInstructuion.SetActive(false);
        mobileInstructuion.SetActive(false);
    }

    private void DeviceVersion()
    {
#if UNITY_STANDALONE_WIN
        FindObjectOfType<Camera>().orthographicSize = 10;
#endif
#if UNITY_ANDROID
        FindObjectOfType<Camera>().orthographicSize = 8;
#endif
    }

    public void Audio()
    {
        if (isAudioMuted)
        {
            isAudioMuted = false;
            audioMixer.SetFloat("MasterVol", 0);
            image.sprite = on;
        }
        else
        {
            isAudioMuted = true;
            audioMixer.SetFloat("MasterVol", -80);
            image.sprite = off;
        }
    }

    public void OpenInstruction()
    {
#if UNITY_ANDROID
        mobileInstructuion.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
pcInstructuion.SetActive(true);
#endif
    }

    public void CloseInstruction()
    {

#if UNITY_ANDROID
        mobileInstructuion.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
pcInstructuion.SetActive(false);
#endif
    }
    public void StartGame()
    {
        //SceneManager.LoadScene(1);

        StartCoroutine(LoadPlay());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadPlay()
    {
        loadingScreen.SetActive(true);

        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(1);

        aSyncLoad.allowSceneActivation = false;

        while (!aSyncLoad.isDone)
        {
            loadingSlider.value = aSyncLoad.progress;
            if(aSyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Press To Continue.....";
                loadingIcon.SetActive(false);
                loadingSlider.gameObject.SetActive(false);

                if (Input.anyKeyDown)
                {
                    aSyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
