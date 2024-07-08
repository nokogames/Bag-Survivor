using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pack.GameData
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public int CurrentLvl;
        public int CurrentSection;

        public PlayerResource playerResource;

        [SerializeField] private List<PlayerSavedUpgradeInfos> playerSavedUpgradeInfos;
        public int GetSavedUpgradeLvl<T>()
        {
            var result = playerSavedUpgradeInfos.FirstOrDefault(x => x.type == typeof(T).ToString());
            if (result != null) return result.Level;
            var info = new PlayerSavedUpgradeInfos(typeof(T).ToString());
            playerSavedUpgradeInfos.Add(info);
            return info.Level;
        }


        public void Load() => SaveManager.LoadData(this);
        public void Save() => SaveManager.SaveData(this);

    }
    [Serializable]
    public class PlayerSavedUpgradeInfos
    {
        public string type;
        public int Level;
        public PlayerSavedUpgradeInfos(string typeName)
        {
            type = typeName;
            Level = 0;
        }
    }
    [Serializable]
    public class SavedLevelData
    {
        public bool IsOpen;
        public float MaxPlayTime;
        public float MaxClearedEnemyPercentage;

    }
    [Serializable]
    public class PlayerResource
    {
        public int GemCount;
        public int CoinCount;
    }

}