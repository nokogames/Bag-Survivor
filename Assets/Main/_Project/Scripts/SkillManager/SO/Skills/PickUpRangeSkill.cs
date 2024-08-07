

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "PickUpRangeSkill", menuName = "ScriptableObjects/SkillSystem/PickUpRangeSkill", order = 0)]
    public class PickUpRangeSkill : SkillBase
    {

        [SerializeField] private List<PickUpRangeByRarity> damageRangeByRarity;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            SkillActivityType = SkillActivityType.Pasive;
        }
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        {
            var result = damageRangeByRarity.First(x => x.rarity == rarity);

            playerUpgradedData.pickUpRange += result.pickUpRange;

            playerUpgradedData.Upgraded();

        }
        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = damageRangeByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt}  +{result.pickUpRange}";
        }
    }

    [Serializable]
    public struct PickUpRangeByRarity
    {
        public SkillRarity rarity;
        public float pickUpRange;
    }
}