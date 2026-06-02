using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AdmobGA_Helper : MonoBehaviour
{
    public static void GA_Log(AdmobGAEvents log)
    {
        switch (log)
        {
            //Initalizing
            case AdmobGAEvents.Initializing:
                LogGAEvent("CAS:Initializing");
                break;
            case AdmobGAEvents.Initialized:
                LogGAEvent("CAS:Initialized");
                break;

            //Request
            case AdmobGAEvents.LoadInterstitialAd:
                LogGAEvent("CAS:iAd:Request");
                break;
            case AdmobGAEvents.LoadOnLoadingInterstitialAd:
                LogGAEvent("CAS:iAdOnLoading:Request");
                break;
            case AdmobGAEvents.LoadVideoAd:
                LogGAEvent("CAS:vAd:Request");
                break;
            case AdmobGAEvents.LoadRewardedAd:
                LogGAEvent("CAS:rAd:Request");
                break;
            case AdmobGAEvents.RequestBannerAd:
                LogGAEvent("CAS:BAd:Request");
                break;
            case AdmobGAEvents.RequestAppOpenAd:
                LogGAEvent("CAS:AppOpen:Request");
                break;


            //LOADED
            case AdmobGAEvents.InterstitialAdLoaded:
                LogGAEvent("CAS:iAd:Loaded");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdLoaded:
                LogGAEvent("CAS:iAdOnLoading:Loaded");
                break;
            case AdmobGAEvents.VideoAdLoaded:
                LogGAEvent("CAS:vAd:Loaded");
                break;
            case AdmobGAEvents.RewardedAdLoaded:
                LogGAEvent("CAS:rAd:Loaded");
                break;
            case AdmobGAEvents.AppOpenAdLoaded:
                LogGAEvent("CAS:AppOpen:Loaded");
                break;
         
            //Show Call
            case AdmobGAEvents.ShowInterstitialAd:
                LogGAEvent("CAS:iAd:ShowCall");
                break;
            case AdmobGAEvents.ShowOnLoadingInterstitialAd:
                LogGAEvent("CAS:iAdOnLoading:ShowCall");
                break;
            case AdmobGAEvents.ShowVideoAd:
                LogGAEvent("CAS:vAd:ShowCall");
                break;
            case AdmobGAEvents.ShowRewardedAd:
                LogGAEvent("CAS:rAd:ShowCall");
                break;
            case AdmobGAEvents.BannerAdStarted:
                LogGAEvent("CAS:BAd:ShowCall");
                break;
            case AdmobGAEvents.ShowAppOpenAd:
                LogGAEvent("CAS:AppOpen:Show");
                break;


            //Will Display
            case AdmobGAEvents.InterstitialAdWillDisplay:
                LogGAEvent("CAS:iAd:WillDisplay");
                break;
            case AdmobGAEvents.VideoAdWillDisplay:
                LogGAEvent("CAS:vAd:WillDisplay");
                break;
            case AdmobGAEvents.RewardedAdWillDisplay:
                LogGAEvent("CAS:rAd:WillDisplay");
                break;
            case AdmobGAEvents.BannerAdWillDisplay:
                LogGAEvent("CAS:BAd:WillDisplay");
                break;

            //Displayed
            case AdmobGAEvents.InterstitialAdDisplayed:
                LogGAEvent("ACASdmob:iAd:Displayed");
                break;
            case AdmobGAEvents.VideoAdDisplayed:
                LogGAEvent("CAS:vAd:Displayed");
                break;
            case AdmobGAEvents.RewardedAdDisplayed:
                LogGAEvent("CAS:rAd:Displayed");
                break;
            case AdmobGAEvents.BannerAdDisplayed:
                LogGAEvent("CAS:BAd:Displayed");
                break;

            //Rewarded Ad Started
            case AdmobGAEvents.RewardedAdStarted:
                LogGAEvent("CAS:rAd:Started");
                break;

            //Rewarded Ad Give Reward
            case AdmobGAEvents.RewardedAdReward:
                LogGAEvent("CAS:rAd:Reward");
                break;

            //No Inventory
            case AdmobGAEvents.RewardedAdNoInventory:
                LogGAEvent("CAS:rAd:NoInventory");
                break;
            case AdmobGAEvents.InterstitialAdNoInventory:
                LogGAEvent("CAS:iAd:NoInventory");
                break;
            case AdmobGAEvents.VideoAdNoInventory:
                LogGAEvent("CAS:vAd:NoInventory");
                break;
            case AdmobGAEvents.AppOpenAdNotLoaded:
                LogGAEvent("CAS:AppOpen:NoInventory");
                break;
           
           //Ad Close
            case AdmobGAEvents.InterstitialAdClosed:
                LogGAEvent("CAS:iAd:Closed");
                break;
            case AdmobGAEvents.VideoAdClosed:
                LogGAEvent("CAS:vAd:Closed");
                break;
            case AdmobGAEvents.RewardedAdClosed:
                LogGAEvent("CAS:rAd:Closed");
                break;
            case AdmobGAEvents.BannerAdRemoved:
                LogGAEvent("CAS:BAd:Removed");
                break;

            //Ad Clicked
            case AdmobGAEvents.InterstitialAdClicked:
                LogGAEvent("CAS:iAd:Clicked");
                break;
            case AdmobGAEvents.VideoAdClicked:
                LogGAEvent("CAS:vAd:Clicked");
                break;
            case AdmobGAEvents.RewardedAdClicked:
                LogGAEvent("CAS:rAd:Clicked");
                break;
            case AdmobGAEvents.BannerAdClicked:
                LogGAEvent("CAS:BAd:Clicked");
                break;

            //Adapters Register
            case AdmobGAEvents.AdaptersInitialized:
                LogGAEvent("CAS:Adapters:Initialized");
                break;

            //Adapters Not Register
            case AdmobGAEvents.AdaptersNotInitialized:
                LogGAEvent("CAS:Adapters:NotInitialized");
                break;


            case AdmobGAEvents.InterstitialAdFailToLoad:
                LogGAEvent("CAS:BAd:Displayed");
                break;
            case AdmobGAEvents.InterstitialAdFailToShow:
                LogGAEvent("CAS:BAd:Displayed");
                break;
            case AdmobGAEvents.ShowMREC:
                LogGAEvent("CAS:BAd:DisplayedMREC");
                break;
            case AdmobGAEvents.HideMREC:
                LogGAEvent("CAS:BAd:HideMREC");
                break;

        }
    }

    public static void LogGAEvent(string log)
    {
        Logger.Log("event Is:: " + log);
        GameAnalytics.NewDesignEvent(log);
    }
}
