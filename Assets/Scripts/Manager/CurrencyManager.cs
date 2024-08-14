namespace UserData.Controller
{
    using System.Collections;
    using System.Collections.Generic;
    using Blueprints;
    using DataManager.MasterData;
    using DataManager.UserData;
    using UnityEngine;
    using UserData.Model;

    public class CurrencyManager : BaseDataManager<UserWallet>
    {
        #region Inject

        private readonly CurrencyBlueprint currencyBlueprint;

        #endregion
        
        public CurrencyManager(MasterDataManager masterDataManager, CurrencyBlueprint currencyBlueprint) : base(masterDataManager)
        {
            this.currencyBlueprint = currencyBlueprint;
        }

        protected override void OnDataLoaded()
        {
            base.OnDataLoaded();

            if (this.Data.WalletLogs == null || this.Data.WalletLogs.Count == 0)
            {
                CreateWallet();
            }
            else LoadWallet();
        }

        void CreateWallet()
        {
            this.Data.WalletLogs = new();

            foreach (var currency in currencyBlueprint.Values)
            {
                WalletLog walletLog = new()
                {
                    CurrencyId     = currency.Id,
                    CurrencyNumber = 0,
                    CurrencyRecord = currency
                };
                
                this.Data.WalletLogs.Add(currency.Id, walletLog);
            }
        }

        void LoadWallet()
        {
            foreach (var currency in currencyBlueprint.Values)
            {
                if (this.Data.WalletLogs.TryGetValue(currency.Id, out WalletLog walletLog))
                {
                    walletLog.CurrencyRecord = currency;
                }
                else
                {
                    walletLog = new()
                    {
                        CurrencyId     = currency.Id,
                        CurrencyNumber = 0,
                        CurrencyRecord = currency
                    };
                
                    this.Data.WalletLogs.Add(currency.Id, walletLog);
                }
            }
        }

        public CurrencyRecord GetCurrencyById(string id)
        {
            return currencyBlueprint[id];
        }

        public WalletLog GetCurrencyLogById(string id)
        {
            return this.Data.WalletLogs[id];
        }
    }

}