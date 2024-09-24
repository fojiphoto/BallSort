using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable, CreateAssetMenu(fileName = "New Skin", menuName = "Create New Skin")]


public class SOSkinInfo : ScriptableObject
{
    public enum SkinIDs
    {
        tube0, tube1, tube2, tube3, tube4, tube5, tube6, tube7, tube8,
    };
    


    [SerializeField] private GameObject skinSpirte;
    public GameObject _skinSpirte { get { return skinSpirte; } }

    public SkinIDs _skinID { get { return skinID; } }
    [SerializeField] private SkinIDs skinID;
    
}
