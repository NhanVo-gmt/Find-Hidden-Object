using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blueprints
{
    using DataManager.Blueprint.BlueprintReader;

    [BlueprintReader("Level")]
    public class LevelBlueprint : GenericBlueprintReaderByRow<string, LevelRecord>
    {
        
    }

    [CsvHeaderKey("Id")]
    public class LevelRecord
    {
        public string                                    Id;
        public bool                                      IsUnlockedByDefault;
        public string                                    Name;
        public BlueprintByRow<string, LevelItemRecord>   LevelItems;
        public BlueprintByRow<string, LevelRewardRecord> LevelRewards;
    }

    [CsvHeaderKey("ItemId")]
    public class LevelItemRecord
    {
        public string ItemId;
        public int    ItemNumber;
    }

    [CsvHeaderKey("RewardId")]
    public class LevelRewardRecord
    {
        public string RewardId;
        public int    RewardNumber;
    }
}
