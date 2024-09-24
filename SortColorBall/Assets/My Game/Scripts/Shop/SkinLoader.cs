using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public Transform playerContainer; // The parent object for the player prefab

    private void Awake()
    {
        if (SkinManager.equippedPrefab != null)
        {
            // Instantiate the equipped prefab in the TestShop scene
            Instantiate(SkinManager.equippedPrefab, playerContainer.position, Quaternion.identity, playerContainer);
        }
        else
        {
            Debug.LogError("No skin equipped. Please select a skin in the shop.");
        }
    }
}
