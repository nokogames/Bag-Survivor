using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{

    public class SkillBase : ScriptableObject
    {

        public SkillCommenUIInfo skillCommenUIInfo;
        public SkillRarityPercentage SkillRarityPercentage;
        public Sprite Icon;
        private SkillRarity _skillRarity;
        public SkillRarity CurrentRarity => _skillRarity;
        public int Level { get; set; } = 0;

        public virtual void OnSelectedSkill() { }

        public void GetRandomRarity(int seed = 0)
        {
            float cumulative = 0;
            var skills = SkillRarityPercentage.SkillRarityPercentageHolders;
            var randomValue = UnityEngine.Random.Range(0, 100);
            foreach (var skill in skills)
            {
                cumulative += skill.rarityPercentage;

                if (randomValue < cumulative)
                {
                    _skillRarity = skill.rarity;
                    return;
                }
            }

            _skillRarity = SkillRarity.Common;
        }
    }
}


