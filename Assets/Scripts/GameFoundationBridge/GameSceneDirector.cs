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
        public static Action StartLoadScene;
        public GameSceneDirector(SignalBus signalBus, IGameAssets gameAssets) : base(signalBus, gameAssets) { CurrentSceneName = SceneName.Loading; }

        #region shortcut

        public UniTask LoadLevelSelectScene()
        {
            StartLoadScene?.Invoke();
            return this.LoadSingleSceneBySceneManagerAsync(SceneName.LevelSelectScene);
        }
        
        public UniTask LoadLevelScene(string id)
        {
            StartLoadScene?.Invoke();
            string levelName = $"Level {id}";
            return this.LoadMultipleSceneBySceneManagerAsync(SceneName.GameScene, SceneName.GameScene, levelName);
        }
        

        #endregion
    }
}