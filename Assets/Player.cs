using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Serializable]                   // 启用序列化
    public struct PlayerStats
    {
        [SerializeField]
        private int movementSpeed;
        [SerializeField]
        private int hitPoints;
        [SerializeField]
        private bool hasHealthPotion;
    }

    [SerializeField]               // 强制序列化privte字段
    private PlayerStats stats;
}
