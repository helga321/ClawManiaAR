using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class UITestIAP : MonoBehaviour
{
    public Fader fader;
    public Text textLog;
    public Text textCoin;

    private int testCoinValue = 500;
    // Use this for initialization
    void Start()
    {
        InitDisplay();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                #if UNITY_IOS && !UNITY_EDITOR
                ICloudPlugin.LoadData();
                #endif
            }
            fader.FadeIn();
        });
    }

    void InitDisplay()
    {
        textCoin.text = "Current coin: " + PlayerData.Instance.PlayerCoin.ToString();
        UnityIAPManager.Instance.OnFinishBuyProduct += OnFinishBuyProduct;
        UnityIAPManager.Instance.OnFailToBuyProduct += OnFailToBuyProduct;
        GoogleAdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
        Fader.OnFadeInFinished += OnFadeInFinished;
    }

	void OnDisable()
	{
        UnityIAPManager.Instance.OnFinishBuyProduct -= OnFinishBuyProduct;
        UnityIAPManager.Instance.OnFailToBuyProduct -= OnFailToBuyProduct;
        GoogleAdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
        Fader.OnFadeInFinished -= OnFadeInFinished;
	}

	public void BuyCoin()
    {
        UnityIAPManager.Instance.BuyConsumable(ShortCode.testCoinID);
    }

    public void ShowAds()
    {
#if UNITY_IOS && !UNITY_EDITOR
        GoogleAdmobManager.Instance.ShowRewardedVideo(AdEvents.FreeCoin);
#endif
#if UNITY_EDITOR
        AddCoin();
#endif
    }

    void OnFinishBuyProduct(string productId)
    {
        if (productId == ShortCode.testCoinID)
        {
            AddCoin();
        }
    }

    void OnFailToBuyProduct(string productId)
    {
        textLog.text = productId;
    }

    void OnFinishWatchVideoAds(AdEvents eventName)
    {
        if (eventName == AdEvents.FreeCoin)
        {
            AddCoin();
        }
    }

    void OnFadeInFinished()
    {

    }


    void AddCoin()
    {
        PlayerData.Instance.PlayerCoin += testCoinValue;
        #if UNITY_IOS && !UNITY_EDITOR
        ICloudPlugin.SaveData(ShortCode.iCloud_PlayerCoin, PlayerData.Instance.PlayerCoin);
        #endif
        textCoin.text = "Current coin: " + PlayerData.Instance.PlayerCoin.ToString();
    }

}
