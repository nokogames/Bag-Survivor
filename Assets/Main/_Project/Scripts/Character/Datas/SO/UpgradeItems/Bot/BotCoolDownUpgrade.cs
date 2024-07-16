
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO.UpgradeItems
{
    [CreateAssetMenu(fileName = "BotCoolDownUpgrade", menuName = "ScriptableObjects/SaveDatas/PlayerUpgradeItem/BotCoolDownUpgrade", order = 0)]
    public class BotCoolDownUpgrade : PlayerMainMenuUpgradeInfo
    {
        public List<BotCoolDownInfo> botCoolDownInfos;
        public override void Upgraded(SavedPlayerData savedPlayerData)
        {
            Lvl++;
            var crr = botCoolDownInfos[Lvl];
            savedPlayerData.botCoolDownTime = crr.coolDown;
            savedPlayerData.botPlayableTime = crr.playableTime;


            base.Upgraded(savedPlayerData);

        }


        public override void Initialize(GameData gameData, SavedPlayerData savedPlayerData)
        {
            base.Initialize(gameData, savedPlayerData);
            base.SetupData<BotCoolDownUpgrade>();

            Lvl = data.Level;


            var crrFireRateInfo = botCoolDownInfos[Lvl];
            savedPlayerData.botCoolDownTime = crrFireRateInfo.coolDown;
            savedPlayerData.botPlayableTime = crrFireRateInfo.playableTime;
        }


        public override int Price
        {
            get
            {
                int nextLvl = Lvl + 1;
                if (nextLvl >= botCoolDownInfos.Count) return -1;
                return botCoolDownInfos[nextLvl].Price;

            }
        }

        public override float Value
        {
            get
            {
                return botCoolDownInfos[Lvl].coolDown;
            }
        }
        public override float NextValue
        {
            get
            {
                return botCoolDownInfos[Lvl + 1].coolDown;
            }
        }

    }

    [Serializable]
    public struct BotCoolDownInfo
    {
        public int Price;
        public float coolDown;
        public float playableTime;
    }
}