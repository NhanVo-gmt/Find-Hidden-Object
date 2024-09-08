using System.Collections;
using System.Collections.Generic;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
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
    public RectTransform   rectTransform;
}

public class GameFooterItemPresenter : BaseUIItemPresenter<GameFooterItemView, GameFooterItemModel>
{
    #region Inject

    private readonly AssetService   assetService;
    private readonly ItemManager    itemManager;
    private readonly IGameAssets    gameAssets;
    private readonly IScreenManager screenManager;

    #endregion

    private GameFooterItemModel model;
    private ItemRecord          itemRecord;
    
    public GameFooterItemPresenter(IGameAssets gameAssets, ItemManager itemManager, AssetService assetService, IScreenManager screenManager) : base(gameAssets)
    {
        this.assetService  = assetService;
        this.itemManager   = itemManager;
        this.gameAssets    = gameAssets;
        this.screenManager = screenManager;
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
        Vector2 position = ScreenPointToAnchoredPosition(Input.mousePosition);
        this.View.middle.anchoredPosition = position;
        
        this.assetService.SpawnAssetCloud(AssetDefaultType.Item, itemRecord.Id, this.View.image.sprite, this.View.middle, 1, () =>
        {

        }, () =>
        {
            UpdateNumText(number);
        });
    }
    
    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.View.rectTransform, screenPosition, this.screenManager.RootUICanvas.UICamera, out localPoint))
        {
            Vector2 pivotOffset = this.View.rectTransform.pivot * this.View.rectTransform.sizeDelta;
            pivotOffset.y += 100;
            return localPoint - (this.View.rectTransform.anchorMax * this.View.rectTransform.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
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