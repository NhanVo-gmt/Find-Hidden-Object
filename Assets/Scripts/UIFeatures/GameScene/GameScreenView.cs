using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
using GameFoundationBridge;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    public Button          hintButton;
    public TextMeshProUGUI hintText;
}

[ScreenInfo(nameof(GameScreenView))]
public class GameScreenPresenter : BaseScreenPresenter<GameScreenView>
{
    #region Inject

    private readonly LevelManager      levelManager;
    private readonly CurrencyManager   currencyManager;
    private readonly DiContainer       diContainer;
    private readonly GameSceneDirector gameSceneDirector;
    private readonly IScreenManager    screenManager;

    #endregion

    private WalletLog walletLog;
    
    public GameScreenPresenter(SignalBus signalBus, LevelManager levelManager, CurrencyManager currencyManager, 
                               DiContainer diContainer, GameSceneDirector gameSceneDirector, IScreenManager screenManager) : base(signalBus)
    {
        this.levelManager      = levelManager;
        this.currencyManager   = currencyManager;
        this.diContainer       = diContainer;
        this.gameSceneDirector = gameSceneDirector;
        this.screenManager     = screenManager;
    }
    
    protected override void OnViewReady()
    {
        base.OnViewReady();
        this.OpenViewAsync().Forget();
    }
    
    public override async UniTask BindData()
    {
        this.View.backButton.onClick.AddListener(GoBackToLevelScreen);
        this.View.hintButton.onClick.AddListener(levelManager.UseHint);

        this.walletLog = currencyManager.GetCurrencyLogById(CurrencyManager.HINT);
        UpdateHintUI(this.walletLog.CurrencyNumber);

        this.walletLog.OnChangedValue += UpdateHintUI;
        
        await PopulateLevelList();
        
        this.View.settingButton.onClick.AddListener(() =>
        {
            this.screenManager.OpenScreen<GameSettingPopupPresenter>();
        });
    }

    void UpdateHintUI(int number)
    {
        this.View.hintText.text = number.ToString();
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
        
        this.walletLog.OnChangedValue -= UpdateHintUI;
        
        this.View.settingButton.onClick.RemoveAllListeners();
        this.View.backButton.onClick.RemoveAllListeners();
        this.View.hintButton.onClick.RemoveAllListeners();
    }
}
