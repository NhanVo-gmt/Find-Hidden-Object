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
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;

    public class LevelManager : BaseDataManager<UserProfile>
    {
        #region Inject

        private readonly LevelBlueprint  levelBlueprint;
        private readonly ScreenManager   screenManager;
        private readonly CurrencyManager currencyManager;

        #endregion
        
        public LevelManager(MasterDataManager masterDataManager, LevelBlueprint levelBlueprint, ScreenManager screenManager, CurrencyManager currencyManager) : base(masterDataManager)
        {
            this.levelBlueprint  = levelBlueprint;
            this.screenManager   = screenManager;
            this.currencyManager = currencyManager;
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

        #region In Game
        
        public void SelectLevel(LevelRecord levelRecord)
        {
            GetCurrentLevelLog().OnCompleted -= ShowCompletedScreen;
                
            this.Data.CurrentLevelId         =  levelRecord.Id;
            GetCurrentLevelLog().OnCompleted += ShowCompletedScreen;
        }

        public void SelectItem(string id, int index)
        {
            LevelLog levelLog = this.Data.levelLogs[GetCurrentLevel().Id];
            levelLog.LevelItemLogs[id].SelectItem(index);
        }

        public void ShowCompletedScreen()
        {
            this.screenManager.OpenScreen<GameCompletePopupPresenter, List<LevelRewardRecord>>(GetCurrentLevel().LevelRewards
                .Values.ToList());
        }

        public void ClaimReward(List<LevelRewardRecord> levelRewardRecords)
        {
            foreach (var levelRewardRecord in levelRewardRecords)
            {
                this.currencyManager.AddCurrency(levelRewardRecord.RewardId, levelRewardRecord.RewardNumber);
            }
        }

        #endregion
    }
    
}
