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
}

public class GameFooterItemPresenter : BaseUIItemPresenter<GameFooterItemView, GameFooterItemModel>
{
    #region Inject

    private readonly IGameAssets gameAssets;
    private readonly ItemManager itemManager;

    #endregion

    private GameFooterItemModel model;
    
    public GameFooterItemPresenter(IGameAssets gameAssets, ItemManager itemManager) : base(gameAssets)
    {
        this.gameAssets  = gameAssets;
        this.itemManager = itemManager;
    }
    
    public override async void BindData(GameFooterItemModel model)
    {
        this.model = model;
        
        ItemRecord itemRecord = itemManager.GetItemRecord(model.levelItemLog.Id);
        
        Sprite sprite = await this.gameAssets.LoadAssetAsync<Sprite>(itemRecord.Sprite);
        this.View.image.sprite = sprite;
        
        UpdateNumText(model.levelItemLog.Progress);

        model.levelItemLog.OnUpdateProgress += UpdateNumText;
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