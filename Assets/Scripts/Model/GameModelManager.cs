using System;
using Domin.Enitiy;
using UnityEngine;

namespace Model
{
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

        public void SaveData(String key, PlayerData value)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
        }
    }
}