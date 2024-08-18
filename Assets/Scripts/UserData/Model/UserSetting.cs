namespace Setting
{
    using System.Collections;
    using System.Collections.Generic;
    using DataManager.LocalData;
    using DataManager.UserData;
    using UnityEngine;

    public class UserSetting : IUserData, ILocalData
    {
        public float MusicValue  = 1f;
        public float SoundValue  = 1f;
        public bool  HapticValue = true;
    }

}