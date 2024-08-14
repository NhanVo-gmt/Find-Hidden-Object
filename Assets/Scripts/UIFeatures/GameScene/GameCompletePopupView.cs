using System.Collections;
using System.Collections.Generic;
using Blueprints;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.MVP;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using GameFoundation.Scripts.Utilities.LogService;
using UnityEngine;
using Zenject;

public class GameCompletePopupView : BaseView
{
    
}

public class GameCompletePopupPresenter : BasePopupPresenter<GameCompletePopupView, LevelRewardRecord>
{
    public GameCompletePopupPresenter(SignalBus signalBus, ILogService logService) : base(signalBus, logService)
    {
    }
    public override UniTask BindData(LevelRewardRecord model)
    {
        return UniTask.CompletedTask;
    }
}
