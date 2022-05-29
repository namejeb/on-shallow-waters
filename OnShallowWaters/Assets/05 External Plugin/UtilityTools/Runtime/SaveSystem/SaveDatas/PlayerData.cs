using UnityEngine;
using System;


namespace NamejebTools.SaveSystem
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        private const string SaveName = "PlayerProfile";
        
        
        public Vector3 PlayerLocation { get; private set; }
        public Quaternion PlayerRotation { get; private set; }

        public PlayerData(Vector3 location, Quaternion rotation)
        {
            PlayerLocation = location;
            PlayerRotation = rotation;
        }

        
        public void Save()
        {
            ISaveData.SaveData(SaveName, this);
        }

        public static void Load()
        {
            SaveDataMain.Current.PlayerData  = (PlayerData) ISaveData.LoadData(SaveName);
        }
    }
}