namespace Analytic
{
    using System.Collections.Generic;
    using DataManager.LocalData;
    using DataManager.UserData;
    using UnityEngine;

    public class AnalyticData : ILocalData, IUserData
    {
        public bool   IsFirstLevelCompleted = false;
        public bool   IsFirstAdsLoaded      = false;
        public int    SessionCount;
        public double UserPlayTime;
        public int    AdsWatchCount;
        public double CoinCollected;
        public double HintCollected;
    }
    
}
