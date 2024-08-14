namespace UIFeatures.LoadingScene
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using DataManager.MasterData;
    using DG.Tweening;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using GameFoundationBridge;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using UserData.Controller;
    using Zenject;
    
    public class SelectLevelScreenView : BaseView
    {
        [Header("Header")]
        public string currencyId;
        public TextMeshProUGUI coinText;
        public Button settingButton;
        public Button shopButton;
        
        [Header("Body")]
        public SelectLevelItemAdapter selectLevelItemAdapter;
    }

    [ScreenInfo(nameof(SelectLevelScreenView))]
    public class SelectLevelScreenPresenter : BaseScreenPresenter<SelectLevelScreenView>
    {
        private readonly LevelManager    levelManager;
        private readonly CurrencyManager currencyManager;
        private readonly DiContainer     diContainer;

        public SelectLevelScreenPresenter(SignalBus signalBus, LevelManager levelManager, CurrencyManager currencyManager, DiContainer diContainer) : base(signalBus)
        {
            this.levelManager    = levelManager;
            this.currencyManager = currencyManager;
            this.diContainer     = diContainer;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.OpenViewAsync().Forget();
        }

        public override async UniTask BindData()
        {
            SetCoinText();
            
            await PopulateLevelList();
        }

        void SetCoinText()
        {
            this.View.coinText.text = this.currencyManager.GetCurrencyLogById(this.View.currencyId).CurrencyNumber.ToString();
        }

        async Task PopulateLevelList()
        {
            List<LevelRecord> levelRecords = this.levelManager.GetAllLevels();
            await this.View.selectLevelItemAdapter.InitItemAdapter(levelRecords.Select(record =>
            {
                return new SelectLevelItemModel(record);
            }).ToList(), this.diContainer);
        }
    }
}