using System;
using UnityEngine;

namespace Pack.GameData
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public int CurrentLvl;
        public int CurrentSection;

        public PlayerResource playerResource;




        public void Load() => SaveManager.LoadData(this);
        public void Save() => SaveManager.SaveData(this);

    }

    [SerializeField]
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