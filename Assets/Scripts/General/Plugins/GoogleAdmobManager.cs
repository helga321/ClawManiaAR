using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using admob;
using GoogleMobileAds.Api;

public enum AdEvents
{
    FreeCoin
}

public class GoogleAdmobManager : MonoBehaviour
{
    private static GoogleAdmobManager instance;

    #region adsIds
    //TODO: REPLACE WITH PRODUCTION IDs LATER
    string iOSRewardedVideoAdsTestID = "ca-app-pub-3940256099942544/1712485313";
    #endregion

    #region events
    public delegate void FinishLoadVideoAds();
    public delegate void FinishWatchVideoAds(AdEvents eventName);
    public event FinishLoadVideoAds OnFinishLoadVideoAds;
    public event FinishWatchVideoAds OnFinishWatchVideoAds;
    #endregion

    //Admob ad;
    AdEvents currentEvent;
    bool videoAdsReady = false;

    private RewardBasedVideoAd rewardedVideo;

    public static GoogleAdmobManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        InitAdmob();
    }

    void OnDisable()
    {
        //ad.rewardedVideoEventHandler -= rewardedVideoEventHandler;
        rewardedVideo.OnAdLoaded -= OnAdLoaded;
        rewardedVideo.OnAdRewarded -= OnAdRewarded;
    }

    void InitAdmob()
    {
        rewardedVideo = RewardBasedVideoAd.Instance;
        rewardedVideo.OnAdLoaded += OnAdLoaded;
        rewardedVideo.OnAdRewarded += OnAdRewarded;
    }

    void OnAdLoaded(object sender, System.EventArgs e)
    {
        if (rewardedVideo.IsLoaded()) videoAdsReady = true;
    }

    void OnAdRewarded(object sender, Reward e)
    {
        videoAdsReady = false;
        if (OnFinishWatchVideoAds != null) OnFinishWatchVideoAds(currentEvent);
    }

    void rewardedVideoEventHandler(string eventName, string msg)
    {
        //      Debug.Log ("eventName:" + eventName + " msg:" + msg);
        //      if(eventName == AdmobEvent.onRewarded){
        //          videoAdsReady = false;
        //          OnFinishWatchVideoAds (currentEvent);
        //      } else if(eventName == AdmobEvent.onAdLoaded){
        //          if(ad.isRewardedVideoReady()){
        //              videoAdsReady = true;
        //          }
        //      }
    }

    public void RequestRewardedVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
#if UNITY_ANDROID
        rewardedVideo.LoadAd(request, androidRewardedVideoID);
#endif
#if UNITY_IOS
        rewardedVideo.LoadAd(request, iOSRewardedVideoAdsTestID);
#endif
    }

    public void ShowRewardedVideo(AdEvents eventName)
    {
        currentEvent = eventName;
        //ad.loadRewardedVideo (androidRewardedVideoID);
        RequestRewardedVideo();
        StartCoroutine(WaitForAds());
    }

    IEnumerator WaitForAds()
    {
        bool adsReady = false;
        //      Debug.Log ("Start waiting. adsReady:" + adsReady);
        while (!adsReady)
        {
            if (videoAdsReady)
            {
                adsReady = true;
            }
            //          Debug.Log ("adsReady:" + adsReady);
            yield return null;
        }
        if (adsReady)
        {
            //          Debug.Log ("ads is ready, playing video");
            adsReady = false;
            if (OnFinishLoadVideoAds != null) OnFinishLoadVideoAds();
            //ad.showRewardedVideo ();
            rewardedVideo.Show();
        }
    }

}
