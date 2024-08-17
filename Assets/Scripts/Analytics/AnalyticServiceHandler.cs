namespace Analytic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core.AnalyticServices;
    using DataManager.MasterData;
    using DataManager.UserData;
    using UnityEngine;
    using Zenject;

    public class AnalyticServiceHandler : BaseDataManager<AnalyticData>, IInitializable, ITickable, IDisposable
    {
        #region Inject

        private readonly IAnalyticServices analyticServices;
        private readonly SignalBus         signalBus;

        #endregion
        
        
        public AnalyticServiceHandler(MasterDataManager masterDataManager, IAnalyticServices analyticServices, SignalBus signalBus) : base(masterDataManager)
        {
            this.analyticServices = analyticServices;
            this.signalBus        = signalBus;
        }
        
        public void Initialize()
        {
            this.analyticServices.Start();
            SubscribeSignals();
        }

        public void SubscribeSignals()
        {
            
        }
        
        public void Tick()
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }

}