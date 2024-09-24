using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdsController : MonoBehaviour
{
    public static AdsController Instance;

    public float InternAdsTime = 120f;
    private const string TimerKey = "InterAdsTimer";

    /*public TMP_Text countdownText;
    public Image imgCd;
    public Image imgBG;*/

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        

        AdManager.instance.ShowBanner();

        // Hide the countdown text initially
        /*if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
            imgCd.gameObject.SetActive(false);
            imgBG.gameObject.SetActive(false);
        }*/
    }



    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }


    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            InternAdsTime -= Time.deltaTime;

            // Update the countdown text if the time is 5 seconds or less
            /*if (InternAdsTime <= 5f)
            {
                if (countdownText != null)
                {
                    countdownText.gameObject.SetActive(true);
                    imgCd.gameObject.SetActive(true);
                    imgBG.gameObject.SetActive(true);
                    imgCd.fillAmount = InternAdsTime * 0.2f;
                    countdownText.text = Mathf.Ceil(InternAdsTime).ToString();
                }
            }
            else
            {
                // Hide the countdown text if the time is more than 5 seconds
                if (countdownText != null)
                {
                    countdownText.gameObject.SetActive(false);
                    imgCd.gameObject.SetActive(false);
                    imgBG.gameObject.SetActive(false);
                }
            }*/

            // Clamp InternAdsTime to 0 and save it in PlayerPrefs
            if (InternAdsTime < 0)
            {
                InternAdsTime = 0;
            }

            /*if (InternAdsTime <= 0)
            {
                AdManager.instance.ShowInter(() =>
                {
                    ResetTime();

                },
                () =>
                {
                    ResetTime();


                }, "Null");
            }*/
            PlayerPrefs.SetFloat(TimerKey, InternAdsTime);

            yield return null;
        }
    }
    public void ResetTime()
    {
        InternAdsTime = 120f;
        PlayerPrefs.SetFloat(TimerKey, InternAdsTime);


    }

    /*private void Update()
    {
        ad();
    }

    public void ad()
    {
        if (InternAdsTime > 0)
        {
            InternAdsTime -= Time.deltaTime;
            if (InternAdsTime <= 5)
            {
                countdownText.gameObject.SetActive(true);
                imgCd.gameObject.SetActive(true);

                imgCd.fillAmount = InternAdsTime * 0.2f;
                countdownText.text = "" + Mathf.Round(InternAdsTime).ToString();
            }
            else
            {
                countdownText.gameObject.SetActive(false);
                imgCd.gameObject.SetActive(false);
            }
        }
        else
        {
            countdownText.gameObject.SetActive(false);
            imgCd.gameObject.SetActive(false);
        }
    }*/
}
