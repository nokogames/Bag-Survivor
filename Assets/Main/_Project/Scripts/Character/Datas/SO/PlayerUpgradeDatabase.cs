

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
        public void Initialize(GameData _gameData)
        {
            PlayerMainMenuUpgradeInfos.ForEach(upgradeInfo => upgradeInfo.Initialize(_gameData));

        }

    }
    [Serializable]
    public struct LevelInfo
    {
        public List<int> XPList;

    }
}