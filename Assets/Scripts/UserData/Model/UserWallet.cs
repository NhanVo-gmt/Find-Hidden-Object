namespace UserData.Model
{
    using System.Collections;
    using System.Collections.Generic;
    using Blueprints;
    using DataManager.LocalData;
    using DataManager.UserData;
    using Newtonsoft.Json;
    using UnityEngine;

    public class UserWallet : IUserData, ILocalData
    {
        public Dictionary<string, WalletLog> WalletLogs;
    }

    public class WalletLog
    {
        public string CurrencyId;
        public int    CurrencyNumber;

        [JsonIgnore] public CurrencyRecord CurrencyRecord;
    }
}