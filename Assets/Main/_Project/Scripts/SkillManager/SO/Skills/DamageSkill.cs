using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "DamageSkill", menuName = "ScriptableObjects/SkillSystem/DamageSkill", order = 0)]
    public class DamageSkill : SkillBase
    {
       
        [SerializeField] private List<DamageAmountByRarity> damageAmountByRarity;
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData,SkillRarity rarity)
        {
            var result = damageAmountByRarity.First(x => x.rarity == rarity);
            playerUpgradedData.damage += result.damageAmount;
             playerUpgradedData.Upgraded();
            Debug.Log("Selected Damage Skill");
        }

          public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = damageAmountByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.damageAmount}";
        }
    }

    [Serializable]
    public struct DamageAmountByRarity
    {
        public SkillRarity rarity;
        public float damageAmount;
    }
}