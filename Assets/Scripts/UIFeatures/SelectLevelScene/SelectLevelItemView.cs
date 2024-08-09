using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blueprints;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.Utilities.ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class SelectLevelItemModel
{
    public readonly LevelRecord levelRecord;

    public SelectLevelItemModel(LevelRecord levelRecord)
    {
        this.levelRecord = levelRecord;
    }
}

public class SelectLevelItemView : TViewMono
{
    public Button          button;
    public TextMeshProUGUI title;
}


public class SelectLevelItemPresenter : BaseUIItemPresenter<SelectLevelItemView, SelectLevelItemModel>
{

    #region Inject

    private readonly ObjectPoolManager objectPoolManager;
    private readonly DiContainer       diContainer;

    #endregion

    private SelectLevelItemModel  model;
    
    public SelectLevelItemPresenter(IGameAssets gameAssets, ObjectPoolManager objectPoolManager, DiContainer diContainer) : base(gameAssets)
    {
        this.objectPoolManager = objectPoolManager;
        this.diContainer       = diContainer;
    }
    
    public override void BindData(SelectLevelItemModel model)
    {
        this.model = model;

        this.View.button.onClick.AddListener(ChangeToGameScene);
        this.View.title.text = $"{model.levelRecord.Id}. {model.levelRecord.Name}";
    }
    

    void ChangeToGameScene()
    {
        
    }
}

