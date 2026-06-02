using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Invoke(nameof(ad), 1);
    }
    public void ad()
    {
        AdsManager.instance.ShowBanner();
    }
}
