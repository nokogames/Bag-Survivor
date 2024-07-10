

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "DamageRangeUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/DamageRangeUpgrade", order = 0)]
    public class DamageRangeUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<DamageRangeInfo> damageRangeInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrDamageInfo = damageRangeInfos[Lvl];
            savedPlayerData.range = crrDamageInfo.Range;
            base.Upgraded(savedPlayerData);
        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {

            base.Initialize(gameData, savedPlayerData);
            base.SetupData<DamageRangeUpgrade>();
            Lvl = data.Level;

            var crrDamageInfo = damageRangeInfos[Lvl];
            savedPlayerData.range = crrDamageInfo.Range;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= damageRangeInfos.Count) return -1;
                return damageRangeInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return damageRangeInfos[Lvl].Range;
            }
        }
        public override float NextValue
        {
            get
            {
                return damageRangeInfos[Lvl + 1].Range;
            }
        }

    }

    [Serializable]
    public struct DamageRangeInfo
    {
        public int Price;
        public float Range;
    }
}