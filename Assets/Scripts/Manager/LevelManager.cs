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

        public List<LevelRecord> GetAllLevels()
        {
            return levelBlueprint.Values.ToList();
        }
    }
    
}
