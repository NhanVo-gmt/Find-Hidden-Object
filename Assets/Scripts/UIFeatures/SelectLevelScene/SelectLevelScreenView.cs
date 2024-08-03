namespace UIFeatures.LoadingScene
{
    using Cysharp.Threading.Tasks;
    using DataManager.MasterData;
    using DG.Tweening;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using GameFoundationBridge;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    [ScreenInfo(nameof(SelectLevelScreenView))]
    public class SelectLevelScreenPresenter : BaseScreenPresenter<SelectLevelScreenView>
    {
        private readonly GameSceneDirector sceneDirector;
        private readonly MasterDataManager masterDataManager;

        public SelectLevelScreenPresenter(SignalBus signalBus, GameSceneDirector sceneDirector, MasterDataManager masterDataManager) : base(signalBus)
        {
            this.sceneDirector     = sceneDirector;
            this.masterDataManager = masterDataManager;
        }

        protected override void OnViewReady()
        {
            base.OnViewReady();
            this.OpenViewAsync().Forget();
        }

        public override UniTask BindData()
        {
            this.View.playBtn.onClick.AddListener(() =>
            {
                this.sceneDirector.LoadGameScene();
            });
            return UniTask.CompletedTask;
        }
    }

    public class SelectLevelScreenView : BaseView
    {
        public Button playBtn;
    }
}