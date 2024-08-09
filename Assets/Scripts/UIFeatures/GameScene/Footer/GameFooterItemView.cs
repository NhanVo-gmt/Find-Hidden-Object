using System.Collections;
using System.Collections.Generic;
using Blueprints;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using TMPro;
using UnityEngine;
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
    public TextMeshProUGUI text;
}

public class GameFooterItemPresenter : BaseUIItemPresenter<GameFooterItemView, GameFooterItemModel>
{
    public GameFooterItemPresenter(IGameAssets gameAssets) : base(gameAssets)
    {
        
    }
    
    public override void BindData(GameFooterItemModel param)
    {
        
    }
}