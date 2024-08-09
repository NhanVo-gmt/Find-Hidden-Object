using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameHeaderView : BaseView
{
    public Button settingButton;
    public Button backButton;
}

public class GameHeaderPresenter : BaseScreenPresenter<GameHeaderView>
{
    public GameHeaderPresenter(SignalBus signalBus) : base(signalBus)
    {
    }
    
    public override UniTask BindData()
    {
        return UniTask.CompletedTask;
    }
}
