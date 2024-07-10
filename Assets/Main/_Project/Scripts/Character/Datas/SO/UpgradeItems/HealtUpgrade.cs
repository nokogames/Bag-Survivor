


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "HealtUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/HealtUpgrade", order = 0)]
    public class HealtUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<HealtInfo> healtInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrDamageInfo = healtInfos[Lvl];
            savedPlayerData.healt = crrDamageInfo.Healt;

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<HealtUpgrade>();
            var crrDamageInfo = healtInfos[Lvl];
            savedPlayerData.healt = crrDamageInfo.Healt;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= healtInfos.Count) return -1;
                return healtInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return healtInfos[Lvl].Healt;
            }
        }
        public override float NextValue
        {
            get
            {
                return healtInfos[Lvl + 1].Healt;
            }
        }

    }

    [Serializable]
    public struct HealtInfo
    {
        public int Price;
        public float Healt;
    }
}