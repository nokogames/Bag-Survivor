using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
namespace _Project.Scripts.SkillManagement.SO
{

    [CreateAssetMenu(fileName = "SkillRarityPercentage", menuName = "ScriptableObjects/SkillSystem/SkillRarityPercentage", order = 0)]
    public class SkillRarityPercentage : ScriptableObject
    {
        public List<SkillRarityPercentageHolder> SkillRarityPercentageHolders;
    }
    [Serializable]
    public struct SkillRarityPercentageHolder
    {
        public SkillRarity rarity;
        [Range(0, 100)] public float rarityPercentage;
    }
}



