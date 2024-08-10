using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueprints;
using GameFoundation.Scripts.AssetLibrary;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
using GameFoundation.Scripts.Utilities.ObjectPool;
using GameFoundationBridge;
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
    private readonly GameSceneDirector gameSceneDirector;

    #endregion

    private SelectLevelItemModel  model;
    
    public SelectLevelItemPresenter(IGameAssets gameAssets, ObjectPoolManager objectPoolManager, DiContainer diContainer, GameSceneDirector gameSceneDirector) : base(gameAssets)
    {
        this.objectPoolManager = objectPoolManager;
        this.diContainer       = diContainer;
        this.gameSceneDirector = gameSceneDirector;
    }
    
    public override void BindData(SelectLevelItemModel model)
    {
        this.model = model;

        this.View.button.onClick.AddListener(async () =>
        {
            this.View.button.onClick.RemoveAllListeners();
            await LoadSelectedLevelScene();
        });
        this.View.title.text = $"{model.levelRecord.Id}. {model.levelRecord.Name}";
    }


    async Task LoadSelectedLevelScene()
    {
        await this.gameSceneDirector.LoadLevelScene(this.model.levelRecord.Id);
    }

    public override void Dispose()
    {
        base.Dispose();
        this.View.button.onClick.RemoveAllListeners();
    }
}

