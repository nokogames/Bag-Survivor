using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "FireBallSkill", menuName = "ScriptableObjects/SkillSystem/FireBallSkill", order = 0)]
    public class FireBallSkill : SkillBase
    {

        [SerializeField] private List<FireBallCountByRarity> fireBallCountByRarity;

        public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        {
        
            var result = fireBallCountByRarity.First(x => x.rarity == rarity);
            inGameSkillController.FireBall(result.count);

            // playerUpgradedData.damage += result.damageAmount;
            // playerUpgradedData.Upgraded();
            Debug.Log("Selected FireBall Skill");
        }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = fireBallCountByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.count}";
        }
        // public override bool ActiveSkill(SkillBase skillBase,Transform transform)
        // {
        //     Debug.Log("Active");
        //     return _playerUpgradedData.ActiveSkill(skillBase);

        // }
        // public override bool DeactivateSkill(SkillBase skillBase)
        // {
        //     Debug.Log("Deactivated");
        //     return _playerUpgradedData.DeactiveSkill(skillBase);
        // }
    }

    [Serializable]
    public struct FireBallCountByRarity
    {
        public SkillRarity rarity;
        public int count;
    }
}