

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "BotDamgeSkill", menuName = "ScriptableObjects/SkillSystem/BotDamgeSkill", order = 0)]
    public class BotDamgeSkill : SkillBase
    {

        [SerializeField] private List<BotDamageAmountByRarity> botDamageAmountByRarity;
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity)
        {
            var result = botDamageAmountByRarity.First(x => x.rarity == rarity);
            playerUpgradedData.botDamage += result.damageAmount;
            playerUpgradedData.Upgraded();
            Debug.Log("Selected Damage Skill");
        }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = botDamageAmountByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt}  +{result.damageAmount}";
        }
    }

    [Serializable]
    public struct BotDamageAmountByRarity
    {
        public SkillRarity rarity;
        public float damageAmount;
    }
}