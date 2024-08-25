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
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
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
        private readonly IScreenManager  screenManager;

        public SelectLevelScreenPresenter(SignalBus signalBus, LevelManager levelManager, CurrencyManager currencyManager, 
                                          DiContainer diContainer, IScreenManager screenManager) : base(signalBus)
        {
            this.levelManager    = levelManager;
            this.currencyManager = currencyManager;
            this.diContainer     = diContainer;
            this.screenManager   = screenManager;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            
            this.OpenViewAsync().Forget();
        }

        public override async UniTask BindData()
        {
            SetCoinText();
            
            this.View.settingButton.onClick.AddListener(OpenSettingScreen);
            
            await PopulateLevelList();
        }

        void OpenSettingScreen()
        {
            this.screenManager.OpenScreen<GameSettingPopupPresenter>();
        }

        void SetCoinText()
        {
            this.View.coinText.text =
                this.currencyManager.GetCurrencyLogById(this.View.currencyId).CurrencyNumber.ToString();
        }

        async Task PopulateLevelList()
        {
            List<LevelRecord> levelRecords = this.levelManager.GetAllLevels();
            await this.View.selectLevelItemAdapter.InitItemAdapter(levelRecords.Select(record =>
            {
                return new SelectLevelItemModel(record);
            }).ToList(), this.diContainer);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.View.settingButton.onClick.RemoveListener(OpenSettingScreen);
            // this.View.shopButton.onClick.RemoveAllListeners();
        }
    }
}