using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine.UI;
using Zenject;

public class GameSettingPopupView : BaseView
{
    public Button musicButton;
    public Button soundButton;
    public Button hapticButton;
    public Button supportButton;
    public Button closeButton;
}

[PopupInfo(nameof(GameSettingPopupView), true, false)]
public class GameSettingPopupPresenter : BasePopupPresenter<GameSettingPopupView>
{
    public GameSettingPopupPresenter(SignalBus signalBus) : base(signalBus)
    {
        
    }
    
    public override UniTask BindData()
    {
        // this.View.soundButton.onClick.AddListener();
        this.View.closeButton.onClick.AddListener(CloseView);
        return UniTask.CompletedTask;
    }

    public override void Dispose()
    {
        base.Dispose();
        
        this.View.musicButton.onClick.RemoveAllListeners();
        this.View.soundButton.onClick.RemoveAllListeners();
        this.View.hapticButton.onClick.RemoveAllListeners();
        // this.View.supportButton.onClick.RemoveAllListeners();
        this.View.closeButton.onClick.RemoveAllListeners();
    }
}

