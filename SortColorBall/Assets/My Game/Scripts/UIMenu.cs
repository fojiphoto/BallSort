using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using static UnityEditor.Progress;
using System.Collections.Generic;
using System.Collections;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;

    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject board;

    [SerializeField] private GameObject noInternetPanel;

    [SerializeField] private GameObject settingBtn;
    [SerializeField] private GameObject shopBtn;

    public List<GameObject> items = new List<GameObject>();
    


    public TMP_Text levelTxt;

    public static UIMenu Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateLevelText(DataManager.Instance.GetLevel());
        AdManager.instance.ShowBanner();
        shopPanel.SetActive(false);

    }

    public void ShowSettingPopup()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.openPopup);
        settingPanel.SetActive(true);
        board.SetActive(true);

    }


    public void ShowNoInternetPopUp(bool isShow)
    {
        if (isShow)
        {
            // Hiển thị panel
            noInternetPanel.SetActive(true);

            // Sau 3 giây, tự động tắt panel
            Invoke("HideNoInternetPanel", 2.5f);
        }
        else
        {
            // Tắt panel ngay lập tức nếu không cần hiển thị
            noInternetPanel.SetActive(false);
        }
    }

    private void HideNoInternetPanel()
    {
        noInternetPanel.SetActive(false);
    }

    public void PlayGame()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

        SceneManager.LoadScene("GamePlay");
    }

    public void CloseShop()
    {
        
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);
        shopPanel.transform.DOMove(new Vector3(0f, 10f, 0f), 1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            // Deactivate the board and the game object after the animation completes
            shopPanel.SetActive(false);
            settingBtn.SetActive(true);
            shopBtn.SetActive(true);
        });

    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        settingBtn.SetActive(false);
        shopBtn.SetActive(false);
        StartCoroutine("ItemAnimation");
        AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);

    }

    IEnumerator ItemAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach (var item in items)
        {
            item.transform.DOScale(1f, 0.75f).SetEase(Ease.OutBounce);
            AudioController.Instance.PlaySound(AudioController.Instance.popItem);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void UpdateLevelText(int level)
    {
        levelTxt.text = "LEVEL " + level.ToString();
    }

    
}

