using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine;
using Zenject;

public class GameScreenView : BaseView
{
    public GameHeaderView gameHeaderView;
    public GameFooterView gameFooterView;
}

public class GameScreenPresenter : BaseScreenPresenter<GameScreenView>
{
    public GameScreenPresenter(SignalBus signalBus) : base(signalBus)
    {
    }
    
    public override UniTask BindData()
    {
        return UniTask.CompletedTask;    
    }

    protected override void OnViewReady()
    {
        base.OnViewReady();
        this.OpenViewAsync().Forget();
    }
}
