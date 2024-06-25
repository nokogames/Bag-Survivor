

using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Character.Datas.SO
{

    [CreateAssetMenu(fileName = "PlayerUpgradeDatabe", menuName = "ScriptableObjects/RuntimeDatas/PlayerUpgradeDatabase", order = 0)]
    public class PlayerUpgradeDatabase : ScriptableObject
    {

        public List<LevelInfo> levelInfos;

    }
    [Serializable]
    public struct LevelInfo
    {
        public List<int> XPList;
    
    }
}