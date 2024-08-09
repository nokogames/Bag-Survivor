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
        public SavedTutorialData SavedTutorialData;
        [SerializeField] private List<SavedLevelData> savedLevelDatas;
        [SerializeField] private List<PlayerSavedUpgradeInfos> playerSavedUpgradeInfos;
        public PlayerSavedUpgradeInfos GetSavedUpgrade<T>()
        {
            var result = playerSavedUpgradeInfos.FirstOrDefault(x => x.type == typeof(T).ToString());
            if (result != null) return result;
            var info = new PlayerSavedUpgradeInfos(typeof(T).ToString());
            playerSavedUpgradeInfos.Add(info);
            return info;
        }

        public SavedLevelData GetSavedLevelData(int lvl)
        {
            var result = savedLevelDatas.FirstOrDefault(x => x.lvl == lvl);
            if (result == null)
            {
                var newData = new SavedLevelData() { lvl = lvl };
                savedLevelDatas.Add(newData);
                if (lvl == 0) newData.IsOpen = true;
                return newData;
            }
            return result;

        }
        public SavedLevelData GetSavedLevelData()
        {
            return GetSavedLevelData(CurrentLvl);
        }

        public void Load() => SaveManager.LoadData(this);
        public void Save() => SaveManager.SaveData(this);

    }
    [Serializable]
    public class SavedTutorialData
    {
        public bool isCompletedSwipeTutorial;
        public bool isCompletedTapTutorial;
        public bool isCompletedGoBtnTutorial;
        internal void CompletedSwipe()
        {
            isCompletedSwipeTutorial = true;
        }
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
        public int lvl;
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