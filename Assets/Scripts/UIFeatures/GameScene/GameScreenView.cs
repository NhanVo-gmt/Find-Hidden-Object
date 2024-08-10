using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;
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
    private readonly LevelManager levelManager;
    private readonly DiContainer  diContainer;
    
    public GameScreenPresenter(SignalBus signalBus, LevelManager levelManager, DiContainer diContainer) : base(signalBus)
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
        Dictionary<string, LevelItemRecord> levelItems = this.levelManager.GetCurrentLevel().LevelItems;
        await this.View.gameFooterItemAdapter.InitItemAdapter(levelItems.Select(keyValuePair =>
        {
            return new GameFooterItemModel(keyValuePair.Value);
        }).ToList(), this.diContainer);
    }
}
