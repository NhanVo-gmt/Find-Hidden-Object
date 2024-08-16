using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.Utilities.LogService;
using GameFoundationBridge;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;
using Zenject;

public class GameCompletePopupView : BaseView
{
    public GameRewardItemAdapter gameRewardItemAdapter;
    public Button                claimBtn;
}

[PopupInfo(nameof(GameCompletePopupView), true, false)]
public class GameCompletePopupPresenter : BasePopupPresenter<GameCompletePopupView, List<LevelRewardRecord>>
{
    #region Inject

    private readonly DiContainer       diContainer;
    private readonly LevelManager      levelManager;
    private readonly GameSceneDirector gameSceneDirector;

    #endregion
    
    private List<LevelRewardRecord> model;
    
    public GameCompletePopupPresenter(SignalBus signalBus, ILogService logService, DiContainer diContainer, LevelManager levelManager, GameSceneDirector gameSceneDirector) : base(signalBus, logService)
    {
        this.diContainer       = diContainer;
        this.levelManager      = levelManager;
        this.gameSceneDirector = gameSceneDirector;
    }
    
    public override async UniTask BindData(List<LevelRewardRecord> model)
    {
        this.model = model;

        await PopulateRewardList();
        this.View.claimBtn.onClick.AddListener(() =>
        {
            this.View.claimBtn.onClick.RemoveAllListeners();
            this.levelManager.ClaimReward();
            this.CloseView();

            this.gameSceneDirector.LoadLevelSelectScene();
        });
    }

    async UniTask PopulateRewardList()
    {
        await this.View.gameRewardItemAdapter.InitItemAdapter(model.Select(levelRewardRecord =>
        {
            return new GameRewardItemModel(levelRewardRecord);
        }).ToList(), this.diContainer);
    }
}
