

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/SpeedUpgrade", order = 0)]
    public class SpeedUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<SpeedUpgradeInfo> fireRateInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrFireRateInfo = fireRateInfos[Lvl];
            savedPlayerData.speed = crrFireRateInfo.Speed;
            base.Upgraded(savedPlayerData);
        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            base.Initialize(gameData, savedPlayerData);
            base.SetupData<SpeedUpgrade>();
            Lvl = data.Level;

            var crrFireRateInfo = fireRateInfos[Lvl];
            savedPlayerData.speed = crrFireRateInfo.Speed;
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
                return fireRateInfos[Lvl].Speed;
            }
        }
        public override float NextValue
        {
            get
            {
                return fireRateInfos[Lvl + 1].Speed;
            }
        }

    }

    [Serializable]
    public struct SpeedUpgradeInfo
    {
        public int Price;
        public float Speed;
    }
}