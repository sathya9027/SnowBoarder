using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] bool testMode = true;

    public static AdManager instance;
    private string gameID = "4770281";
    private CrashDetector crashDetector;

    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameID, testMode);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Unity Ad Error: " + message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
            case ShowResult.Skipped:

                break;
            case ShowResult.Finished:
                crashDetector.ContinueGame();
                break;
        }
    }

    public void ShowAd(CrashDetector crashDetector)
    {
        this.crashDetector = crashDetector;

        Advertisement.Show("RetryVideo");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Untiy Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ad Ready");
    }

}
