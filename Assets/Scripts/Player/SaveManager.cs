using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace ABKaspo.Game
{
    [Serializable]
    public class PlayerConfig
    {
        public string playerName = "Player";
        public bool firstJoin = true;
        public bool rtx = false;
        public bool vsync = false;
        public bool fullscreen = true;
        public FullScreenMode fullScreenMode;
        public Resolution resolution = new Resolution();
        public float fps;

        public PlayerConfig()
        {
            resolution.width = 1920;
            resolution.height = 1080;
        }
    }
    public class SaveManager : MonoBehaviour
    {
        // Set PlayerPref Key
        private const string playerConfigKey = "playerConfigKey";

        // Save Player config function
        public static void SavePlayerConfig(PlayerConfig data)
        {
            string jsonData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(playerConfigKey, jsonData);
            PlayerPrefs.Save();
        }

        // Load Player config function
        public static PlayerConfig LoadPlayerConfig()
        {
            if (PlayerPrefs.HasKey(playerConfigKey))
            {
                string jsonData = PlayerPrefs.GetString(playerConfigKey);
                return JsonUtility.FromJson<PlayerConfig>(jsonData);
            }
            else
            {
                return new PlayerConfig();
            }
        }
    }
}