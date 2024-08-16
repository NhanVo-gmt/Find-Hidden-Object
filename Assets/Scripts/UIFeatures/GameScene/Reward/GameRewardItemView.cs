using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;

public class GameRewardItemModel
{
    public LevelRewardRecord levelRewardRecord;

    public GameRewardItemModel(LevelRewardRecord levelRewardRecord)
    {
        this.levelRewardRecord = levelRewardRecord;
    }
}

public class GameRewardItemView : TViewMono
{
    public Image rewardImg;
    public TextMeshProUGUI  rewardText;
}

public class GameRewardItemPresenter : BaseUIItemPresenter<GameRewardItemView, GameRewardItemModel>
{
    #region Inject

    private IGameAssets     gameAssets;
    private CurrencyManager currencyManager;

    #endregion
    
    private GameRewardItemModel model;
    
    public GameRewardItemPresenter(IGameAssets gameAssets, CurrencyManager currencyManager) : base(gameAssets)
    {
        this.gameAssets      = gameAssets;
        this.currencyManager = currencyManager;
    }
    
    public override async void BindData(GameRewardItemModel model)
    {
        this.model = model;

        CurrencyRecord currencyRecord = currencyManager.GetCurrencyById(model.levelRewardRecord.RewardId);
        this.View.rewardImg.sprite = await gameAssets.LoadAssetAsync<Sprite>(currencyRecord.Icon);
        this.View.rewardText.text  = model.levelRewardRecord.RewardNumber.ToString();
    }
}
