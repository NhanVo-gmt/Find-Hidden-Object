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
        public string                          Id;
        public bool                            IsUnlockedByDefault;
        public string                          Name;
        public BlueprintByRow<LevelItemRecord> LevelItems;
    }

    [CsvHeaderKey("ItemId")]
    public class LevelItemRecord
    {
        public string ItemId;
        public int    Number;
    }
}
