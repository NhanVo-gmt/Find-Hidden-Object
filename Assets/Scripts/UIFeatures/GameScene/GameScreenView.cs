using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundationBridge;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;
using UserData.Model;
using Zenject;

public class GameScreenView : BaseView
{
    [Header("Header")]
    public Button settingButton;

    public Button backButton;

    [Header("Footer")]
    public GameFooterItemAdapter gameFooterItemAdapter;
}

[ScreenInfo(nameof(GameScreenView))]
public class GameScreenPresenter : BaseScreenPresenter<GameScreenView>
{
    private readonly LevelManager  levelManager;
    private readonly DiContainer   diContainer;
    private readonly GameSceneDirector gameSceneDirector;
    
    public GameScreenPresenter(SignalBus signalBus, LevelManager levelManager, DiContainer diContainer, GameSceneDirector gameSceneDirector) : base(signalBus)
    {
        this.levelManager      = levelManager;
        this.diContainer       = diContainer;
        this.gameSceneDirector = gameSceneDirector;
    }
    
    protected override void OnViewReady()
    {
        base.OnViewReady();
        this.OpenViewAsync().Forget();
    }
    
    public override async UniTask BindData()
    {
        this.View.backButton.onClick.AddListener(GoBackToLevelScreen);
        await PopulateLevelList();
    }

    void GoBackToLevelScreen()
    {
        this.gameSceneDirector.LoadLevelSelectScene();
    }
    
    async Task PopulateLevelList()
    {
        Dictionary<string, LevelItemLog> levelItems = this.levelManager.GetCurrentLevelLog().LevelItemLogs;
        await this.View.gameFooterItemAdapter.InitItemAdapter(levelItems.Select(keyValuePair =>
        {
            return new GameFooterItemModel(keyValuePair.Value);
        }).ToList(), this.diContainer);
    }

    public override void Dispose()
    {
        base.Dispose();
        this.View.settingButton.onClick.RemoveAllListeners();
        this.View.backButton.onClick.RemoveAllListeners();
    }
}
