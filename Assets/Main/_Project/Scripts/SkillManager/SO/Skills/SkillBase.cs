using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{

    public class SkillBase : ScriptableObject
    {
        public string Name;
        public string InfoTxt;
        public SkillCommenUIInfo skillCommenUIInfo;
        public SkillRarityPercentage SkillRarityPercentage;
        public Sprite Icon;
        // private SkillRarity _skillRarity;
        // public SkillRarity CurrentRarity => _skillRarity;
        public int Level { get; set; } = 0;

        public virtual void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController) { }
        public virtual string GetInfoTxt(SkillRarity rarity) => "";

        public SkillRarity GetRandomRarity(int seed = 0)
        {
            SkillRarity _skillRarity = SkillRarity.Common;
            float cumulative = 0;
            var skills = SkillRarityPercentage.SkillRarityPercentageHolders;
            var randomValue = UnityEngine.Random.Range(0, 100);
            foreach (var skill in skills)
            {
                cumulative += skill.rarityPercentage;

                if (randomValue < cumulative)
                {
                    _skillRarity = skill.rarity;
                    return _skillRarity;
                }
            }

            return _skillRarity;
        }
    }
}


