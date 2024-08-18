namespace Setting
{
    using System.Collections;
    using System.Collections.Generic;
    using DataManager.MasterData;
    using DataManager.UserData;
    using GameFoundation.Scripts.Models;
    using GameFoundation.Scripts.Utilities;
    using UnityEngine;

    public class SettingManager : BaseDataManager<UserSetting>
    {
        private SoundSetting soundSetting;
        
        public SettingManager(MasterDataManager masterDataManager) : base(masterDataManager)
        {
            
        }

        protected override void OnDataLoaded()
        {
            base.OnDataLoaded();

            soundSetting.SoundValue.Value = this.Data.SoundValue;
            soundSetting.MusicValue.Value = this.Data.MusicValue;
        }
    }

}