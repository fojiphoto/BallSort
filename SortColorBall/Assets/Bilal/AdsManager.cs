using System;
using System.Collections;
using UnityEngine;
using GamePix;


public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitialWithoutConditions(string placement)
    {
        Gpx.Ads.InterstitialAd(OnInterstitalAdSuccess);
    }
    [AOT.MonoPInvokeCallback(typeof(Gpx.gpxCallback))]
    public static void OnInterstitalAdSuccess()
    {
        Gpx.Log("SUCCESS");
    }

    private static Action onSuccess;
    public void ShowRewardedAd(Action success)
    {
        onSuccess = success;
        Gpx.Ads.RewardAd(OnRewardAdSuccess, OnRewardAdFail);
    }

    [AOT.MonoPInvokeCallback(typeof(Gpx.gpxCallback))]
    public static void OnRewardAdSuccess()
    {
        onSuccess?.Invoke();
        onSuccess = null;
    }

    [AOT.MonoPInvokeCallback(typeof(Gpx.gpxCallback))]
    public static void OnRewardAdFail()
    {
        onSuccess = null;
    }

    
}
