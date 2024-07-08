
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;


namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "PickUpRangeUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/PickUpRangeUpgrade", order = 0)]
    public class PickUpRangeUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<PickUpRangeInfo> picUpRangeInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crrPickUpRangeInfo = picUpRangeInfos[Lvl];
            savedPlayerData.pickUpRange = crrPickUpRangeInfo.Range;

        }


        public override void Initialize(GameData gameData)
        {
            Lvl = gameData.GetSavedUpgradeLvl<PickUpRangeUpgrade>();
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= picUpRangeInfos.Count) return -1;
                return picUpRangeInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return picUpRangeInfos[Lvl].Range;
            }
        }
        public override float NextValue
        {
            get
            {
                return picUpRangeInfos[Lvl + 1].Range;
            }
        }

    }

    [Serializable]
    public struct PickUpRangeInfo
    {
        public int Price;
        public float Range;
    }
}
