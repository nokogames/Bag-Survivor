using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "DamageUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/DamageUpgrade", order = 0)]
    public class DamageUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<DamageInfo> damageInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrDamageInfo = damageInfos[Lvl];
            savedPlayerData.damage = crrDamageInfo.DamageAmount;

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<DamageUpgrade>();
            var crrDamageInfo = damageInfos[Lvl];
            savedPlayerData.damage = crrDamageInfo.DamageAmount;
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
                return damageInfos[Lvl].DamageAmount;
            }
        }
        public override float NextValue
        {
            get
            {
                return damageInfos[Lvl + 1].DamageAmount;
            }
        }

    }

    [Serializable]
    public struct DamageInfo
    {
        public int Price;
        public float DamageAmount;
    }
}