

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "BotFireRateUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/BotFireRateUpgrade", order = 0)]
    public class BotFireRateUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<BotFireRateInfo> botFireRateInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrFireRateInfo = botFireRateInfos[Lvl];
            savedPlayerData.botFirerate = crrFireRateInfo.BotFireRate;

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<BotFireRateInfo>();
            var crrFireRateInfo = botFireRateInfos[Lvl];
            savedPlayerData.botFirerate = crrFireRateInfo.BotFireRate;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= botFireRateInfos.Count) return -1;
                return botFireRateInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return botFireRateInfos[Lvl].BotFireRate;
            }
        }
        public override float NextValue
        {
            get
            {
                return botFireRateInfos[Lvl + 1].BotFireRate;
            }
        }

    }

    [Serializable]
    public struct BotFireRateInfo
    {
        public int Price;
        public float BotFireRate;
    }
}