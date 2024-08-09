namespace UserData.Model
{
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
        public Dictionary<string, List<LevelItemLog>> levelItemLogs;
        
        [JsonIgnore] public LevelRecord                      levelRecord;
    }

    public class LevelItemLog
    {
        public string Id;
        public int    Index;
        public bool   HasPicked;
        
        [JsonIgnore] public LevelItemRecord levelItemRecord;
    }
}