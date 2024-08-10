namespace Blueprints
{
    using System;
    using System.Collections.Generic;
    using DataManager.Blueprint.BlueprintReader;

    [BlueprintReader("Item")]
    public class ItemBlueprint : GenericBlueprintReaderByRow<string, ItemRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class ItemRecord
    {
        public string Id;
        public string Sprite;
    }
}