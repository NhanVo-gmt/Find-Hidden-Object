using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIContexts
{
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using GameFoundation.Scripts.UIModule.Utilities;

    public class GameScreenInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.Container.InitScreenManually<GameScreenPresenter>();

            this.Container.Bind<ItemManager>().AsSingle();
        }
    }
}
