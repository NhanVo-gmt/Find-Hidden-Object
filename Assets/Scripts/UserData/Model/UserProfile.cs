namespace UserData.Model
{
    using System;
    using System.Collections.Generic;
    using Blueprints;
    using DataManager.LocalData;
    using DataManager.UserData;
    using Newtonsoft.Json;

    public  class UserProfile : IUserData, ILocalData
    {
        public string                       CurrentLevelId { get; set; } = "";
        public Dictionary<string, LevelLog> levelLogs;

        public void RegisterEvent()
        {
            foreach (var levelLog in levelLogs.Values)
            {
                levelLog.RegisterEvent();
            }
        }
    }
    
    public class LevelLog
    {
        public string                           Id;
        public Dictionary<string, LevelItemLog> LevelItemLogs;
        public int                              Progress;
        public bool                             IsCompleted;

        [JsonIgnore] public LevelRecord LevelRecord;

        public void RegisterEvent()
        {
            foreach (var item in LevelItemLogs.Values)
            {
                item.OnCompleted += OnItemCompleted;
            }
        }

        public void OnItemCompleted()
        {
            Progress++;
            if (Progress == LevelItemLogs.Count)
            {
                IsCompleted = true;
            }
        }
    }

    public class LevelItemLog
    {
        public string                Id;
        public int                   Progress;
        public Dictionary<int, bool> PickedDict;
        
        [JsonIgnore] public LevelItemRecord LevelItemRecord;
        [JsonIgnore] public Action<int>     OnUpdateProgress;
        [JsonIgnore] public Action          OnCompleted;

        public void SelectItem(int index)
        {
            if (PickedDict[index]) return;
            
            Progress++;
            PickedDict[index] = true;
            OnUpdateProgress?.Invoke(Progress);

            if (Progress == PickedDict.Count)
            {
                OnCompleted?.Invoke();
            }
        }
    }
}