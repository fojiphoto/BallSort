using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "New ball Skin", menuName = "Create New ball Skin")]

public class SOBallSkinInfo : ScriptableObject
{
    public enum SkinIDs
    {
        ball0, ball1, ball2, ball3, ball4, ball5, ball6, ball7, ball8,
    };

    [SerializeField] private Sprite skinSpirte;
    public Sprite _skinSpirte { get { return skinSpirte; } }

    public SkinIDs _skinID { get { return skinID; } }
    [SerializeField] private SkinIDs skinID;
}
