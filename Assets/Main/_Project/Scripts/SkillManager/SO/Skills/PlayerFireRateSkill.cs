

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "PlayerFireRateSkill", menuName = "ScriptableObjects/SkillSystem/PlayerFireRateSkill", order = 0)]
    public class PlayerFireRateSkill : SkillBase
    {

        [SerializeField] private List<PlayerFireRateByRarity> playerFireRateByRarity;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            SkillActivityType = SkillActivityType.Pasive;
        }
        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        {
            var result = playerFireRateByRarity.First(x => x.rarity == rarity);
            playerUpgradedData.firerate += result.firerate;
            playerUpgradedData.Upgraded();
            Debug.Log("Selected Player FireRate Skill");
        }
        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = playerFireRateByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.firerate}";
        }
    }

    [Serializable]
    public struct PlayerFireRateByRarity
    {
        public SkillRarity rarity;
        public float firerate;
    }
}