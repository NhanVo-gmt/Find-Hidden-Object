using System;
using System.Collections;
using System.Collections.Generic;
using DataManager.MasterData;
using DataManager.UserData;
using UnityEngine;
using UserData.Model;

namespace UserData.Controller
{
    using System.Linq;
    using Blueprints;

    public class LevelManager : BaseDataManager<UserProfile>
    {
        #region Inject

        private readonly LevelBlueprint levelBlueprint;

        #endregion
        
        public LevelManager(MasterDataManager masterDataManager, LevelBlueprint levelBlueprint) : base(masterDataManager)
        {
            this.levelBlueprint = levelBlueprint;
        }

        protected override void OnDataLoaded()
        {
            base.OnDataLoaded();

            if (String.IsNullOrWhiteSpace(this.Data.CurrentLevelId))
            {
                LoadDefaultLevel();
                return;
            }
        }

        void LoadDefaultLevel()
        {
            this.Data.CurrentLevelId = levelBlueprint.FirstOrDefault().Value.Id;
        }

        public List<LevelRecord> GetAllLevels()
        {
            return levelBlueprint.Values.ToList();
        }

        public LevelRecord GetCurrentLevel()
        {
            return levelBlueprint[this.Data.CurrentLevelId];
        }

        public LevelLog GetCurrentLevelLog()
        {
            return this.Data.levelLogs[GetCurrentLevel().Id];
        }

        public LevelRecord GetLevel(string Id)
        {
            return levelBlueprint[Id];
        }

        public void SelectLevel(LevelRecord levelRecord)
        {
            this.Data.CurrentLevelId = levelRecord.Id;
        }
    }
    
}
