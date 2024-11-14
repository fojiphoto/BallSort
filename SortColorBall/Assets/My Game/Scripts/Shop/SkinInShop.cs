using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinInShop : MonoBehaviour
{
    public static SkinInShop Instance;
    public SOSkinInfo skinInfo;

    public Image skinImage;

    public bool isSkinUnlocked;
    public Image itemLocked;
    //public Image selected_img;
    public Image ads_img;

    public TMP_Text buttonText;


    //public Button buyBtn;

    [SerializeField] private bool isFreeSkin;

    private void Awake()
    {
        Instance = this;
        //skinImage.sprite = skinInfo._skinSpirte;
        if (isFreeSkin)
        {
            PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
            //selected_img.gameObject.SetActive(true);

        }
        
        IsSkinUnlocked();


    }

    

    public void OnButtonPress()
    {
        if (isSkinUnlocked)
        {
            //equip
            SkinManager.Instance.EquipSkin(this);
            BallSortColorAudioController.Instance.PlaySound(BallSortColorAudioController.Instance.clickBtn);

            //itemSelected.SetActive(true);

        }
        else
        {
            //buy
            
            PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
            IsSkinUnlocked();
            


        }
    }

    public void OnButtonPressReward()
    {

        // Kiểm tra xem skin đã được mở khóa hay chưa
        if (isSkinUnlocked)
        {
            // Nếu skin đã được mở khóa, chỉ trang bị skin mà không cần xem quảng cáo
            OnButtonPress();
            return;
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIMenu.Instance.ShowNoInternetPopUp(true);
            Debug.Log("khong co internet");
            return;
        }
        //Nadeem Ads BallSort
        //AdManager.instance.ShowReward(() =>
        //{
        //    AudioController.Instance.PlaySound(AudioController.Instance.clickBtn);
        //    OnButtonPress();


        //}, () =>
        //{


        //}, "YourPlacementID");

        OnButtonPress();
    }

    public void IsSkinUnlocked()
    {
        if(PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            //buyBtn.gameObject.SetActive(false);
            buttonText.text = "EQUIP";
            skinImage.gameObject.SetActive(true);
            itemLocked.gameObject.SetActive(false);
            //selected_img.gameObject.SetActive(true);
            ads_img.gameObject.SetActive(false);
        }
    }


}
