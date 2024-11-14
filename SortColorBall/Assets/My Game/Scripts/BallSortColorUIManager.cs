using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class BallSortColorUIManager : MonoBehaviour
{
    public static BallSortColorUIManager Instance;
    public GameObject WinPopUp;
    public GameObject effectLevelComplete;
    public Image adsImg;
    public TMP_Text levelTxt;

    public GameObject guideSelectBall;
    public GameObject guideMoveBall;



    public GameObject guidelevel2;





    private void Awake()
    {

        Instance = this;
        WinPopUp.SetActive(false);
        CheckGuide(DataManager.Instance.GetLevel());
    }
    public void CheckGuide(int level)
    {
        if (level == 1)
        {

            ShowSelectGuide();
        }
        else if (level == 2)
        {
            ShowGuideLevel2();


        }
        else
        {

            GuideOff();
        }
    }


    public void ShowSelectGuide()
    {
        guideSelectBall.SetActive(true);
        guideMoveBall.SetActive(false);

    }

    public void ShowGuideLevel2()
    {

        guideSelectBall.SetActive(false);
        guideMoveBall.SetActive(false);
        guidelevel2.SetActive(true);
    }

    public void ShowMoveGuide()
    {
        guideSelectBall.SetActive(false);
        guideMoveBall.SetActive(true);
    }

    public void GuideOff()
    {
        guideSelectBall.SetActive(false);
        guideMoveBall.SetActive(false);
        guidelevel2.SetActive(false);
    }
    public void TurnAds(bool isTurn)
    {
        if (!isTurn)
        {
            adsImg.gameObject.SetActive(false);
        }
        else
        {
            adsImg.gameObject.SetActive(true);
        }
    }
    public void Reload()
    {
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        SceneManager.LoadScene("GamePlay");

    }

    public IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(effectLevelComplete);

        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.win);
        WinPopUp.SetActive(true);

    }

    public void NextLevel()
    {
        BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

        WinPopUp.SetActive(false);

    }

    public void UpdateLevelText(int level)
    {
        levelTxt.text = "LEVEL " + level.ToString();
    }


}
