





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "BotSpeedUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/BotSpeedUpgrade", order = 0)]
    public class BotSpeedUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<BotSpeedUpgradeInfo> speedInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crr = speedInfos[Lvl];
            savedPlayerData.speed = crr.BotSpeed;
            base.Upgraded(savedPlayerData);
        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            base.Initialize(gameData, savedPlayerData);
            base.SetupData<BotSpeedUpgrade>();
            Lvl = data.Level;

            var crrFireRateInfo = speedInfos[Lvl];
            savedPlayerData.botSpeed = crrFireRateInfo.BotSpeed;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= speedInfos.Count) return -1;
                return speedInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return speedInfos[Lvl].BotSpeed;
            }
        }
        public override float NextValue
        {
            get
            {
                return speedInfos[Lvl + 1].BotSpeed;
            }
        }

    }

    [Serializable]
    public struct BotSpeedUpgradeInfo
    {
        public int Price;
        public float BotSpeed;
    }
}