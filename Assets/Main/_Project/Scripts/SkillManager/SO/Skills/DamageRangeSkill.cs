


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "DamageRangeSkill", menuName = "ScriptableObjects/SkillSystem/DamageRangeSkill", order = 0)]
    public class DamageRangeSkill : SkillBase
    {

        [SerializeField] private List<RangeByRarity> damageRangeByRarity;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            SkillActivityType = SkillActivityType.Pasive;
        }
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        {
            var result = damageRangeByRarity.First(x => x.rarity == rarity);

            playerUpgradedData.range += result.range;
            playerUpgradedData.Upgraded();
            Debug.Log("Selected Damage Range Skill");
        }
        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = damageRangeByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt}  +{result.range}";
        }
    }

    [Serializable]
    public struct RangeByRarity
    {
        public SkillRarity rarity;
        public float range;
    }
}