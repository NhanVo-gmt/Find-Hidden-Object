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
    using UnityEngine;
    using UnityEngine.UI;
    using UserData.Controller;
    using Zenject;
    
    public class SelectLevelScreenView : BaseView
    {
        public SelectLevelItemAdapter selectLevelItemAdapter;
    }

    [ScreenInfo(nameof(SelectLevelScreenView))]
    public class SelectLevelScreenPresenter : BaseScreenPresenter<SelectLevelScreenView>
    {
        private readonly LevelManager levelManager;
        private readonly DiContainer  diContainer;

        public SelectLevelScreenPresenter(SignalBus signalBus, LevelManager levelManager, DiContainer diContainer) : base(signalBus)
        {
            this.levelManager = levelManager;
            this.diContainer  = diContainer;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.OpenViewAsync().Forget();
        }

        public override async UniTask BindData()
        {
            await PopulateLevelList();
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