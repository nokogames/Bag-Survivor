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
        [SerializeField] private GameObject ballPref;
        private FireBallCountByRarity _skillRarityData;
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
        public override bool ActiveSkill(Transform playerTransform, SkillRarity skillRarity)
        {

            _playerTransform = playerTransform;

            Debug.Log("Active");

            var result = _playerUpgradedData.ActiveSkill(this);
            if (result)
            {
                _skillRarityData = fireBallCountByRarity.First(x => x.rarity == skillRarity);
                _fireRate = _skillRarityData.ballFireRate;
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
        internal override void FixedTick()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _fireRate) return;
            _crrTime = 0;

            CreateFireBall();

        }
        private void CreateFireBall()
        {
            var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(ballPref).GetComponent<FireBallBehavior>();
            fireBallBehavior.Initialise(_playerTransform);
            fireBallBehavior.gameObject.SetActive(true);
        }

    }

    [Serializable]
    public struct FireBallCountByRarity
    {
        public SkillRarity rarity;
        public int count;
        public float ballFireRate;
    }
}