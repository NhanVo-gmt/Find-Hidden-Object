namespace Blueprints
{
    using System.Collections;
    using System.Collections.Generic;
    using DataManager.Blueprint.BlueprintReader;
    using UnityEngine;

    [BlueprintReader("Currency")]
    public class CurrencyBlueprint : GenericBlueprintReaderByRow<string, CurrencyRecord> 
    {
        
    }

    [CsvHeaderKey("Id")]
    public class CurrencyRecord
    {
        public string Id;
        public string Icon;
    }

}