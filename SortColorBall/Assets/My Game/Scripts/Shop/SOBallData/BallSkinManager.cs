using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSkinManager : MonoBehaviour
{
    public static GameObject equippedPrefab { get; private set; }
    public static BallSkinManager Instance;
    public const string BallSkinPref = "ballskinPref";
    public SOBallSkinInfo[] allBallSkins;
    [SerializeField] private Transform ballSkinInShopPanelsParent;
    [SerializeField] private List<BallSkinInShop> BallSkinInShopPanels = new List<BallSkinInShop>();
    private Button currentlyEquipSkinButton;

    
    private void Awake()
    {
        Instance = this;
        foreach (Transform s in ballSkinInShopPanelsParent)
        {
            if (s.TryGetComponent(out BallSkinInShop ball))
            {
                BallSkinInShopPanels.Add(ball);
            }
        }
        EquipPreviousBallSkin();

        BallSkinInShop ballSkinInShop = Array.Find(BallSkinInShopPanels.ToArray(), dummyFind => dummyFind.skinInfo._skinSpirte == equippedPrefab);

        currentlyEquipSkinButton = ballSkinInShop.GetComponentInChildren<Button>();
        currentlyEquipSkinButton.interactable = false;
    }

    public void EquipSkinBall(BallSkinInShop BalllSkinInfoShop)
    {

        //equippedPrefab = BalllSkinInfoShop.skinInfo._skinSpirte;

        PlayerPrefs.SetInt(BallSkinPref, (int)BalllSkinInfoShop.skinInfo._skinID);
        //PlayerPrefs.Save(); // Đảm bảo lưu PlayerPrefs
        if (currentlyEquipSkinButton != null)
        {
            currentlyEquipSkinButton.interactable = true;



        }
        currentlyEquipSkinButton = BalllSkinInfoShop.GetComponentInChildren<Button>();
        currentlyEquipSkinButton.interactable = false;

    }

    private void EquipPreviousBallSkin()
    {
        int lastSkinused = PlayerPrefs.GetInt(BallSkinPref, (int)SOBallSkinInfo.SkinIDs.ball0);
        BallSkinInShop skinEquipPanel = Array.Find(BallSkinInShopPanels.ToArray(), dummyFind => (int)dummyFind.skinInfo._skinID == lastSkinused);

        EquipSkinBall(skinEquipPanel);
    }
}
