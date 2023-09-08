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
        public PlayerData LoadData(String key) => PlayerPrefs.HasKey(key)
            ? JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(key))
            : new PlayerData();

        public void SaveData(Dictionary<String, PlayerData> data) =>
            PlayerPrefs.SetString(data.First().Key, JsonUtility.ToJson(data.First().Value));
    }
}