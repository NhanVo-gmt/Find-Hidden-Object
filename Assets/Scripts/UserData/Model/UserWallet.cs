namespace UserData.Model
{
    using System;
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
        [JsonIgnore] public Action<int>    OnChangedValue;

        public void AddCurrencyNumber(int value)
        {
            CurrencyNumber += value;
            Debug.Log($"{CurrencyId}: {CurrencyNumber}");
            
            OnChangedValue?.Invoke(CurrencyNumber);
        }
        
        public void UseCurrencyNumber(int value)
        {
            CurrencyNumber -= value;
            Debug.Log($"{CurrencyId}: {CurrencyNumber}");
            
            OnChangedValue?.Invoke(CurrencyNumber);
        }
    }
}