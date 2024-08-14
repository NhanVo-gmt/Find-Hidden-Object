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
    }
    
    public class LevelLog
    {
        public string                            Id;
        public Dictionary<string, LevelItemLog> LevelItemLogs;

        [JsonIgnore] public LevelRecord LevelRecord;
    }

    public class LevelItemLog
    {
        public string                Id;
        public int                   Progress;
        public Dictionary<int, bool> PickedDict;
        
        [JsonIgnore] public LevelItemRecord LevelItemRecord;
        [JsonIgnore] public Action<int>     OnUpdateProgress;

        public void SelectItem(int index)
        {
            if (PickedDict[index]) return;
            
            Progress++;
            PickedDict[index] = true;
            OnUpdateProgress?.Invoke(Progress);
        }
    }
}