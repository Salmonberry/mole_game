using System;
using System.Collections.Generic;
using System.Linq;
using Domin.Enitiy;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameModelManager : MonoBehaviour
    {
        public PlayerData playerData;
        public PlayerData LoadData(String key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(key));

                return playerData;
            }
            else
            {
                playerData = new PlayerData();

                return playerData;
            }
        }

        public void SaveData(Dictionary<String,PlayerData> data) => PlayerPrefs.SetString(data.First().Key, JsonUtility.ToJson(data.First().Value));
    }
}