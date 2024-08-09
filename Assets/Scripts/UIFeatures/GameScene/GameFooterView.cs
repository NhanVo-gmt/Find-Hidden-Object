using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine;
using Zenject;

public class GameFooterView : BaseView
{
    public GameFooterItemAdapter gameFooterAdapter;
}

public class GameFooterPresenter : BaseScreenPresenter<GameFooterView>
{
    public GameFooterPresenter(SignalBus signalBus) : base(signalBus)
    {
    }
    
    public override UniTask BindData()
    {
        return UniTask.CompletedTask;
    }
}
