

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "AKWeaponSkill", menuName = "ScriptableObjects/SkillSystem/AKWeaponSkill", order = 0)]
    public class AKWeaponSkill : SkillBase
    {

        [SerializeField] private List<AKWeaponSkillByRarity> _akWeaponByRarity;


        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = _akWeaponByRarity.First(x => x.rarity == rarity);
            //+{result.count}
            return $"{InfoTxt} ";
        }
        public override bool ActiveSkill(SkillBase skillBase, Transform playerTransform)
        {
            _playerTransform = playerTransform;

            Debug.Log("Active");
            return _playerUpgradedData.ActiveSkill(skillBase);

        }
        public override bool DeactivateSkill(SkillBase skillBase)
        {
            Debug.Log("Deactivated");
            return _playerUpgradedData.DeactiveSkill(skillBase);
        }

        internal override void FixedTick()
        {
            CustomExtentions.ColorLog($"Player Pos {_playerTransform.position}", Color.blue);
        }
    }

    [Serializable]
    public struct AKWeaponSkillByRarity
    {
        public SkillRarity rarity;

    }
}