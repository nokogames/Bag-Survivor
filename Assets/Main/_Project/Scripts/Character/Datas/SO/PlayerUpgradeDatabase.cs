

using System;
using System.Collections.Generic;
using Pack.GameData;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO
{

    [CreateAssetMenu(fileName = "PlayerUpgradeDatabe", menuName = "ScriptableObjects/RuntimeDatas/PlayerUpgradeDatabase", order = 0)]
    public class PlayerUpgradeDatabase : ScriptableObject
    {

        public List<LevelInfo> levelInfos;
        public List<PlayerMainMenuUpgradeInfo> PlayerMainMenuUpgradeInfos;
        public List<PlayerMainMenuUpgradeInfo> BotMainMenuUpgradeInfos;
        public void Initialize(GameData _gameData, SavedPlayerData savedPlayer)
        {
            PlayerMainMenuUpgradeInfos.ForEach(upgradeInfo => upgradeInfo.Initialize(_gameData, savedPlayer));
            BotMainMenuUpgradeInfos.ForEach(upgradeInfo => upgradeInfo.Initialize(_gameData, savedPlayer));

        }

    }
    [Serializable]
    public struct LevelInfo
    {
        public List<int> XPList;

    }
}