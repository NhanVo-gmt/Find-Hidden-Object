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
            }

            if (this.Data.levelLogs == null)
            {
                CreateLevelLogSave();
            }
            else
            {
                LoadLevelLogSave();
            }
            
            this.Data.RegisterEvent();
        }

        void LoadDefaultLevel()
        {
            this.Data.CurrentLevelId = levelBlueprint.FirstOrDefault().Value.Id;
        }
        
        private void CreateLevelLogSave()
        {
            this.Data.levelLogs = new();
            foreach (var level in GetAllLevels())
            {
                this.Data.levelLogs[level.Id] = new()
                {
                    Id = level.Id,
                    LevelRecord =  level,
                    IsCompleted = false,
                    LevelItemLogs = new(),
                };

                foreach (var levelItem in level.LevelItems.Values)
                {
                    LevelItemLog itemLog = new()
                    {
                        Id = levelItem.ItemId,
                        Progress = 0,
                        LevelItemRecord = levelItem,
                        PickedDict = new()
                    };
                    for (int i = 0; i < levelItem.ItemNumber; i++)
                    {
                        itemLog.PickedDict[i] = false;
                    }
                    
                    this.Data.levelLogs[level.Id].LevelItemLogs.Add(levelItem.ItemId, itemLog);
                    Debug.Log($"Level {level.Id}: Create {levelItem.ItemId}");
                }
            }
        }

        private void LoadLevelLogSave()
        {
            foreach (var levelLog in this.Data.levelLogs.Values)
            {
                LevelRecord levelRecord = GetLevelRecord(levelLog.Id);
                levelLog.LevelRecord = levelRecord;

                foreach (var levelItemLog in levelLog.LevelItemLogs.Values)
                {
                    LevelItemRecord levelItemRecord = levelRecord.LevelItems[levelItemLog.Id];
                    levelItemLog.LevelItemRecord = levelItemRecord;
                }
            }
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

        public LevelRecord GetLevelRecord(string Id)
        {
            return levelBlueprint[Id];
        }

        public void SelectLevel(LevelRecord levelRecord)
        {
            this.Data.CurrentLevelId = levelRecord.Id;
        }

        #region In Game

        public void SelectItem(string id, int index)
        {
            LevelLog levelLog = this.Data.levelLogs[GetCurrentLevel().Id];
            levelLog.LevelItemLogs[id].SelectItem(index);
        }

        #endregion
    }
    
}
