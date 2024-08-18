using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameSettingPopupView : BaseView
{
    [Header("Sound")]
    public Button soundButton;
    public Image  soundImage;

    [Header("Music")]
    public Button musicButton;
    public Image musicImage;
    
    [Header("Haptic")]
    public Button hapticButton;
    public Image hapticImage;
    
    [Header("Support")]
    public Button supportButton;
    public Image supportImage;
}

[PopupInfo(nameof(GameSettingPopupView), true, false)]
public class GameSettingPopupPresenter : BasePopupPresenter<GameSettingPopupView>
{
    public GameSettingPopupPresenter(SignalBus signalBus) : base(signalBus)
    {
        
    }
    
    public override UniTask BindData()
    {
        return UniTask.CompletedTask;
    }
}

