
using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.SkillManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.SkillManagement.SO
{

    [CreateAssetMenu(fileName = "SkillCommenUIInfo", menuName = "ScriptableObjects/SkillSystem/SkillCommenUIInfo", order = 1)]
    public class SkillCommenUIInfo : ScriptableObject
    {
        public List<SkillRarityIconHolder> skillRarityIconHolders;
        public Sprite GetSprite(SkillRarity skillRarity)
        {
            return skillRarityIconHolders.First(x => x.rarity == skillRarity).rarityBgImage;
        }
    }
    [Serializable]
    public struct SkillRarityIconHolder
    {
        public SkillRarity rarity;
        public Sprite rarityBgImage;
    }
}