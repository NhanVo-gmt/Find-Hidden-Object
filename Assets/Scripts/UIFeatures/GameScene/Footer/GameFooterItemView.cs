using System.Collections;
using System.Collections.Generic;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.Utilities.Extension;
using TMPro;
using Transactions.Blueprint;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserData.Model;

public class GameFooterItemModel
{
    public readonly LevelItemLog levelItemLog;

    public GameFooterItemModel(LevelItemLog levelItemLog)
    {
        this.levelItemLog = levelItemLog;
    }
}

public class GameFooterItemView : TViewMono
{
    public Image           image;
    public TextMeshProUGUI numText;
    public RectTransform   middle;
}

public class GameFooterItemPresenter : BaseUIItemPresenter<GameFooterItemView, GameFooterItemModel>
{
    #region Inject

    private readonly IGameAssets gameAssets;
    private readonly ItemManager itemManager;

    #endregion

    private GameFooterItemModel model;
    private AssetService        assetService;
    private ItemRecord          itemRecord;
    
    public GameFooterItemPresenter(IGameAssets gameAssets, ItemManager itemManager, AssetService assetService) : base(gameAssets)
    {
        this.gameAssets   = gameAssets;
        this.itemManager  = itemManager;
        this.assetService = assetService;
    }
    
    public override async void BindData(GameFooterItemModel model)
    {
        this.model = model;
        
        itemRecord = itemManager.GetItemRecord(model.levelItemLog.Id);
        
        Sprite sprite = await this.gameAssets.LoadAssetAsync<Sprite>(itemRecord.Sprite);
        this.View.image.sprite = sprite;
        
        UpdateNumText(model.levelItemLog.Progress);

        model.levelItemLog.OnUpdateProgress += OnUpdateProgress;
        
        this.assetService.RegisterAssetFlyingTarget(AssetDefaultType.Item, model.levelItemLog.Id, this.View.GetComponent<RectTransform>());
    }

    void OnUpdateProgress(int number)
    {
        this.assetService.SpawnAssetCloud(AssetDefaultType.Item, itemRecord.Id, this.View.image.sprite, this.View.middle, 1, () =>
        {

        }, () =>
        {
            UpdateNumText(number);
        });
    }

    void UpdateNumText(int number)
    {
        this.View.numText.text = $"{number}/{model.levelItemLog.LevelItemRecord.ItemNumber}";
    }

    public override void Dispose()
    {
        base.Dispose();
        
        model.levelItemLog.OnUpdateProgress -= UpdateNumText;
    }
}