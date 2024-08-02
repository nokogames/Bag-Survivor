

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

        [SerializeField] private List<LighteningSkillByRarity> lighteningSkillDataByRarity;
        [SerializeField] private GameObject prefab;
        private LighteningSkillByRarity _skillRarityData;
        // public override void OnSelectedSkill(PlayerUpgradedData playerUpgradedData, SkillRarity rarity, InGameSkillController inGameSkillController)
        // {
        //     var result = fireBallCountByRarity.First(x => x.rarity == rarity);
        //     inGameSkillController.Lightening(result.count);

        //     // playerUpgradedData.damage += result.damageAmount;
        //     // playerUpgradedData.Upgraded();
        //     Debug.Log("Selected FireBall Skill");
        // }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = lighteningSkillDataByRarity.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{result.count}";
        }


        public override bool ActiveSkill(Transform playerTransform, SkillRarity skillRarity)
        {

            _playerTransform = playerTransform;

            Debug.Log("Active");

            var result = _playerUpgradedData.ActiveSkill(this);
            if (result)
            {
                _skillRarityData = lighteningSkillDataByRarity.First(x => x.rarity == skillRarity);
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
        internal override void FixedTick()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _fireRate) return;
            _crrTime = 0;

            CreateLightening();

        }
        private void CreateLightening()
        {
            var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(prefab).GetComponent<LighteningSkillBehaviour>();
            fireBallBehavior.Initialise(_playerTransform);
            fireBallBehavior.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public struct LighteningSkillByRarity
    {
        public SkillRarity rarity;
        public int count;
        public float fireRate;
    }
}