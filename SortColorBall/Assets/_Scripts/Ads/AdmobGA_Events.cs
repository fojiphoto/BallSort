//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void AdmobRewardUserDelegate(object sender, Reward args);

public enum AdmobGAEvents
{
    Initializing,
    Initialized,

    RequestBannerAd,
    BannerAdLoaded,
    BannerAdStarted,
    BannerAdRemoved,
    BannerAdFailedToLoad,
    BannerAdWillDisplay,
    BannerAdDisplayed,
    BannerAdClicked,

    LoadOnLoadingInterstitialAd,
    OnLoadingInterstitialAdLoaded,
    ShowOnLoadingInterstitialAd,

    LoadInterstitialAd,
    LoadVideoAd,
    LoadRewardedAd,
    InterstitialAdLoaded,
    VideoAdLoaded,
    RewardedAdLoaded,
    ShowInterstitialAd,
    ShowVideoAd,
    ShowRewardedAd,
    InterstitialAdWillDisplay,
    VideoAdWillDisplay,
    RewardedAdWillDisplay,
    InterstitialAdDisplayed,
    VideoAdDisplayed,
    RewardedAdDisplayed,
    InterstitialAdNoInventory,
    VideoAdNoInventory,
    RewardedAdNoInventory,
    InterstitialAdFailToLoad,
    InterstitialAdFailToShow,
    RewardedAdStarted,

    RewardedAdReward,

    InterstitialAdClicked,
    VideoAdClicked,
    RewardedAdClicked,

    InterstitialAdClosed,
    VideoAdClosed,
    RewardedAdClosed,

    AdaptersInitialized,
    AdaptersNotInitialized,
    
    RequestAppOpenAd,
    AppOpenAdLoaded,
    ShowAppOpenAd,
    AppOpenAdNotLoaded,

    ShowMREC,
    HideMREC
}
