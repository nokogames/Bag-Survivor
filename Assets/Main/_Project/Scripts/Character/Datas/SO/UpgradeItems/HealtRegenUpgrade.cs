




using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "HealtRegenUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/HealtRegenUpgrade", order = 0)]
    public class HealtRegenUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<HealtRegenInfo> healtRegenInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrDamageInfo = healtRegenInfos[Lvl];
            savedPlayerData.healtRegenAmount = crrDamageInfo.HealtRegenAmount;
            base.Upgraded(savedPlayerData);
        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            base.Initialize(gameData, savedPlayerData);
            base.SetupData<HealtRegenUpgrade>();
            Lvl = data.Level;

            var crrDamageInfo = healtRegenInfos[Lvl];
            savedPlayerData.healtRegenAmount = crrDamageInfo.HealtRegenAmount;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= healtRegenInfos.Count) return -1;
                return healtRegenInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return healtRegenInfos[Lvl].HealtRegenAmount;
            }
        }
        public override float NextValue
        {
            get
            {
                return healtRegenInfos[Lvl + 1].HealtRegenAmount;
            }
        }

    }

    [Serializable]
    public struct HealtRegenInfo
    {
        public int Price;
        public float HealtRegenAmount;
    }
}