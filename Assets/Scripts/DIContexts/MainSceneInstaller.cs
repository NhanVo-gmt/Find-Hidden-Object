namespace DIContexts
{
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using GameFoundation.Scripts.UIModule.Utilities;
    using UIFeatures.LoadingScene;
    using UserData.Controller;

    public class MainSceneInstaller : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.Container.InitScreenManually<SelectLevelScreenPresenter>();
        }
    }
}