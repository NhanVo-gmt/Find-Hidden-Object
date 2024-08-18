using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.Utilities.LogService;
using GameFoundation.Scripts.Utilities.ObjectPool;
using GameFoundationBridge;
using UnityEngine;
using UnityEngine.UI;
using UserData.Controller;
using UserData.Model;
using Zenject;

public class GameCompletePopupView : BaseView
{
    public Transform          gameRewardContent;
    public GameRewardItemView gameRewardItemPrefab;
    public Button             claimBtn;
}

[PopupInfo(nameof(GameCompletePopupView), true, false)]
public class GameCompletePopupPresenter : BasePopupPresenter<GameCompletePopupView, LevelLog>
{
    #region Inject

    private readonly DiContainer       diContainer;
    private readonly LevelManager      levelManager;
    private readonly ObjectPoolManager objectPoolManager;

    #endregion
    
    private LevelLog                 model;
    private List<GameRewardItemView> rewardItemViews;
    
    public GameCompletePopupPresenter(SignalBus signalBus, ILogService logService, DiContainer diContainer, LevelManager levelManager, ObjectPoolManager objectPoolManager) : base(signalBus, logService)
    {
        this.diContainer       = diContainer;
        this.levelManager      = levelManager;
        this.objectPoolManager = objectPoolManager;
    }
    
    public override async UniTask BindData(LevelLog model)
    {
        this.model = model;

        await PopulateRewardList();
        this.View.claimBtn.onClick.AddListener(() =>
        {
            this.View.claimBtn.onClick.RemoveAllListeners();
            this.levelManager.ClaimReward(model);
            this.CloseView();

        });
    }

    async UniTask PopulateRewardList()
    {
        this.rewardItemViews = this.model.LevelRecord.LevelRewards.Values.Select(record =>
        {
            var instance = this.objectPoolManager.Spawn(this.View.gameRewardItemPrefab, this.View.gameRewardContent);
            var position = instance.transform.localPosition;
            position.z                       = 0;
            instance.transform.localPosition = position;
            instance.transform.localScale    = Vector3.one;
            var view = instance.GetComponent<GameRewardItemView>();
            this.diContainer.Inject(view);
            view.BindData(record).Forget();
            instance.gameObject.SetActive(true);
            return view;
        }).ToList();
    }
}
