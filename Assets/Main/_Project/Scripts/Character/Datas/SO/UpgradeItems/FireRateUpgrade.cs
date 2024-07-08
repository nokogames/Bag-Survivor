
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "FireRateUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/FireRateUpgrade", order = 0)]
    public class FireRateUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<FireRateInfo> fireRateInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrFireRateInfo = fireRateInfos[Lvl];
            savedPlayerData.firerate = crrFireRateInfo.FireRate;

        }


        public override void Initialize(GameData gameData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<FireRateInfo>();
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= fireRateInfos.Count) return -1;
                return fireRateInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return fireRateInfos[Lvl].FireRate;
            }
        }
        public override float NextValue
        {
            get
            {
                return fireRateInfos[Lvl + 1].FireRate;
            }
        }

    }

    [Serializable]
    public struct FireRateInfo
    {
        public int Price;
        public float FireRate;
    }
}