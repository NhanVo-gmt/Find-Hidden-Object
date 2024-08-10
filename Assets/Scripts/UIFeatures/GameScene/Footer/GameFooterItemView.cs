using System.Collections;
using System.Collections.Generic;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.Utilities.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameFooterItemModel
{
    public readonly LevelItemRecord levelItemRecord;

    public GameFooterItemModel(LevelItemRecord levelItemRecord)
    {
        this.levelItemRecord = levelItemRecord;
    }
}

public class GameFooterItemView : TViewMono
{
    public Image           image;
    public TextMeshProUGUI numText;
}

public class GameFooterItemPresenter : BaseUIItemPresenter<GameFooterItemView, GameFooterItemModel>
{
    #region Inject

    private readonly IGameAssets gameAssets;
    private readonly ItemManager itemManager;

    #endregion
    
    public GameFooterItemPresenter(IGameAssets gameAssets, ItemManager itemManager) : base(gameAssets)
    {
        this.gameAssets  = gameAssets;
        this.itemManager = itemManager;
    }
    
    public override async void BindData(GameFooterItemModel model)
    {
        ItemRecord itemRecord = itemManager.GetItemRecord(model.levelItemRecord.ItemId);
        
        Sprite sprite = await this.gameAssets.LoadAssetAsync<Sprite>(itemRecord.Sprite);
        this.View.image.sprite = sprite;
        this.View.numText.text         = $"{0}/{model.levelItemRecord.Number}";
    }
}