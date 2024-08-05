

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "GrenadeSkill", menuName = "ScriptableObjects/SkillSystem/GrenadeSkill", order = 0)]
    public class GrenadeSkill : SkillBase
    {

        [SerializeField] private List<GrenadeSkillDataByRarity> grenadeDataByRarity;
        [SerializeField] private GameObject prefab;
        private GrenadeSkillDataByRarity _skillRarityData;
        // public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        // {

        //     var result = fireBallCountByRarity.First(x => x.rarity == rarity);
        //     inGameSkillController.FireBall(result.count);

        //     // playerUpgradedData.damage += result.damageAmount;
        //     // playerUpgradedData.Upgraded();
        //     Debug.Log("Selected FireBall Skill");
        // }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = grenadeDataByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.count}";
        }
        public override bool ActiveSkill(Transform playerTransform, SkillRarity skillRarity)
        {

            _playerTransform = playerTransform;

            Debug.Log("Active");

            var result = _playerUpgradedData.ActiveSkill(this);
            if (result)
            {
                _skillRarityData = grenadeDataByRarity.First(x => x.rarity == skillRarity);
                _fireRate = _skillRarityData.fireRate;
            }
            return result;
        }

        public override bool DeactivateSkill()
        {
            Debug.Log("Deactivated");
            return _playerUpgradedData.DeactiveSkill(this);
        }

        private float _fireRate;
        private float _crrTime = 0;
      public override float SkillPercentage =>  _crrTime /_fireRate ;
        internal override void AttactFixedTick()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _fireRate) return;
            _crrTime = 0;

            CreateGrenade();

        }
        private void CreateGrenade()
        {
            var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(prefab).GetComponent<GrenadeBehaviour>();
            fireBallBehavior.Initialise(_playerTransform);
            fireBallBehavior.gameObject.SetActive(true);
        }

    }

    [Serializable]
    public struct GrenadeSkillDataByRarity
    {
        public SkillRarity rarity;
        public int count;
        public float fireRate;
    }
}