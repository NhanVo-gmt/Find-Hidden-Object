namespace GameFoundationBridge
{
    using System;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using UnityEngine.SceneManagement;
    using Zenject;

    public static class SceneName
    {
        public const string Loading          = "0.LoadingScene";
        public const string LevelSelectScene = "1.LevelSelectScene";
        public const string GameScene        = "2.GameScene";
    }

    public class GameSceneDirector : SceneDirector
    {
        private LoadingScreenView loadingScreenView;
        public GameSceneDirector(SignalBus signalBus, IGameAssets gameAssets, LoadingScreenView loadingScreenView) :
            base(signalBus, gameAssets)
        {
            CurrentSceneName       = SceneName.Loading;
            this.loadingScreenView = loadingScreenView;
        }

        #region shortcut
        
        public UniTask FirstLoadLevelSelectScene()
        {
            return this.LoadSingleSceneBySceneManagerAsync(SceneName.LevelSelectScene);
        }

        public async UniTask LoadLevelSelectScene()
        {
            await loadingScreenView.Show();
            await this.LoadSingleSceneBySceneManagerAsync(SceneName.LevelSelectScene);
        }
        
        public async UniTask LoadLevelScene(string id)
        {
            await loadingScreenView.Show();
            
            string levelName = $"Level {id}";
            await this.LoadMultipleSceneBySceneManagerAsync(SceneName.GameScene, SceneName.GameScene, levelName);
        }
        

        #endregion
    }
}