using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;

public class ApplodealInititalizer : MonoBehaviour, IAppodealInitializationListener
{
    
        private void Start()
        {
            int adTypes = Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO | Appodeal.MREC;
            string appKey = "a1f84b2f1dd76c2b8b65c7613ea87145fa0a793f35505de7";
            Appodeal.initialize(appKey, adTypes, this);
        }

        public void onInitializationFinished(List<string> errors) {}
    
}
