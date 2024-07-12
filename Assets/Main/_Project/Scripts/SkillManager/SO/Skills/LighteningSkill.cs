

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "LighteningSkill", menuName = "ScriptableObjects/SkillSystem/LighteningSkill", order = 0)]
    public class LighteningSkill : SkillBase
    {

        [SerializeField] private List<LighteningSkillByRarity> fireBallCountByRarity;
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        {
            var result = fireBallCountByRarity.First(x => x.rarity == rarity);
            inGameSkillController.Lightening(result.count);

            // playerUpgradedData.damage += result.damageAmount;
            // playerUpgradedData.Upgraded();
            Debug.Log("Selected FireBall Skill");
        }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = fireBallCountByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.count}";
        }
    }

    [Serializable]
    public struct LighteningSkillByRarity
    {
        public SkillRarity rarity;
        public int count;
    }
}