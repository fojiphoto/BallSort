using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkinManager : MonoBehaviour
{
    public static GameObject equippedPrefab { get; private set; }
    public static SkinManager Instance;
    //tube
    public SOSkinInfo[] allSkins;
    private const string tubeSkinPref = "skinPref";
    [SerializeField] private Transform tubeSkinInShopPanelsParent;
    [SerializeField] private List<SkinInShop> skinInShopPanels = new List<SkinInShop>();

    //ball
    



    //[SerializeField] private Transform playerContainer;
    //private GameObject currentPlayer;

    private Button currentlyEquipSkinButton;
    private void Awake()
    {
        Instance = this;

        foreach (Transform s in tubeSkinInShopPanelsParent)
        {
            if(s.TryGetComponent(out SkinInShop skinInShop))
            {
                skinInShopPanels.Add(skinInShop);
            }
        }
        EquipPreviousSkin();

        SkinInShop skinEquipPanel = Array.Find(skinInShopPanels.ToArray(), dummyFind => dummyFind.skinInfo._skinSpirte == equippedPrefab);

        currentlyEquipSkinButton = skinEquipPanel.GetComponentInChildren<Button>();
        currentlyEquipSkinButton.interactable = false;




        


        
    }

    private void EquipPreviousSkin()
    {
        string lastSkinused = PlayerPrefs.GetString(tubeSkinPref, SOSkinInfo.SkinIDs.tube0.ToString());
        SkinInShop skinEquipPanel = Array.Find(skinInShopPanels.ToArray(), dummyFind => dummyFind.skinInfo._skinID.ToString() == lastSkinused);

        EquipSkin(skinEquipPanel);
    }
    public void EquipSkin(SkinInShop skinInfoShop)
    {
        
        equippedPrefab = skinInfoShop.skinInfo._skinSpirte;
        
        PlayerPrefs.SetString(tubeSkinPref, skinInfoShop.skinInfo._skinID.ToString());

        if(currentlyEquipSkinButton != null)
        {
            currentlyEquipSkinButton.interactable = true;
            


        }
        currentlyEquipSkinButton = skinInfoShop.GetComponentInChildren<Button>();
        currentlyEquipSkinButton.interactable = false;
        
    }

    


}
