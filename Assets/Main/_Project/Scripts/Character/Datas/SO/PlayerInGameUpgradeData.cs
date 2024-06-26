
using System;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO
{

    [CreateAssetMenu(fileName = "PlayerRuntimeUpgradeData", menuName = "ScriptableObjects/RuntimeDatas/PlayerRuntimeUpgradeData", order = 0)]
    public class PlayerRuntimeUpgradeData : ScriptableObject
    {
        public RunTimePlayerData runTimePlayerData;
        public SavedPlayerData savedPlayerData;
    }
    [Serializable]
    public class SavedPlayerData
    {
    }

    [Serializable]
    public class RunTimePlayerData
    {
        public int StartLvl;
        public int NextLvl;
        public float BarFillAmount;
        public void Reset()
        {
            StartLvl = 0;
            NextLvl = 1;
            BarFillAmount = 0;
        }
        public void LevelUp()
        {
            StartLvl++;
            NextLvl++;
            BarFillAmount = 0;
        }
    }

}