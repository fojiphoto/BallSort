using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAinit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }

    
}
