






using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "BotDamageUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/BotDamageUpgrade", order = 0)]
    public class BotDamageUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<BotDamageUpgradeInfo> damageInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crr = damageInfos[Lvl];
            savedPlayerData.botDamage = crr.BotDamage;

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<BotDamageUpgrade>();
            var crrFireRateInfo = damageInfos[Lvl];
            savedPlayerData.botDamage = crrFireRateInfo.BotDamage;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= damageInfos.Count) return -1;
                return damageInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return damageInfos[Lvl].BotDamage;
            }
        }
        public override float NextValue
        {
            get
            {
                return damageInfos[Lvl + 1].BotDamage;
            }
        }

    }

    [Serializable]
    public struct BotDamageUpgradeInfo
    {
        public int Price;
        public float BotDamage;
    }
}