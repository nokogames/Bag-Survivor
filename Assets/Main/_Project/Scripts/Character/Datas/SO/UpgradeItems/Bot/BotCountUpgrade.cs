







using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "BotCountUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/BotCountUpgrade", order = 0)]
    public class BotCountUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<BotCountInfo> botCountInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crr = botCountInfos[Lvl];
            savedPlayerData.botCount = crr.BotCount;

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<BotCountUpgrade>();
            var crrFireRateInfo = botCountInfos[Lvl];
            savedPlayerData.botCount = crrFireRateInfo.BotCount;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= botCountInfos.Count) return -1;
                return botCountInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return botCountInfos[Lvl].BotCount;
            }
        }
        public override float NextValue
        {
            get
            {
                return botCountInfos[Lvl + 1].BotCount;
            }
        }

    }

    [Serializable]
    public struct BotCountInfo
    {
        public int Price;
        public int BotCount;
    }
}