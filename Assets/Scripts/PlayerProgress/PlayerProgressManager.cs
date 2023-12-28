using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace PlayerProgress
{
    public class PlayerProgressManager
    {
        private const string PlayerProgressPrefsKey = "PlayerProgress";

        public PlayerProgressData Data;
        
        public void LoadProgress()
        {
            if (!PlayerPrefs.HasKey(PlayerProgressPrefsKey))
            {
                CreateDefault();
                return;
            }

            string json = PlayerPrefs.GetString(PlayerProgressPrefsKey);
            try
            {
                Data = JsonConvert.DeserializeObject<PlayerProgressData>(json);
            }
            catch (Exception e)
            {
                CreateDefault();
            }
        }

        public void SaveProgress()
        {
            string json = JsonConvert.SerializeObject(Data);
            PlayerPrefs.SetString(PlayerProgressPrefsKey, json);
        }

        private void CreateDefault()
        {
            Data = new PlayerProgressData
            {
                Money = 0,
                SelectedShip = 0,
                BoughtShips = new List<int> { 0 },
                CompletedLevels = new List<int>(),
            };
        }
    }
}