

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
        private AKWeaponSkillByRarity _skillRarityData;
        private TimeRater _timeRater;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            _timeRater = TimeRater.Init(100);

        }
        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = _akWeaponByRarity.First(x => x.rarity == rarity);
            //+{result.count}
            return $"{InfoTxt} ";
        }
        public override bool ActiveSkill(Transform playerTransform, SkillRarity skillRarity)
        {
            _playerTransform = playerTransform;

            Debug.Log("Active");

            var result = _playerUpgradedData.ActiveSkill(this);
            if (result)
            {
                _skillRarityData = _akWeaponByRarity.First(x => x.rarity == skillRarity);
                //  _fireRate = _skillRarityData.fireRate;
                _timeRater.SetTimeRate(_skillRarityData.fireRate);
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

        public override float SkillPercentage => _timeRater.Percentage;
        internal override void AttactFixedTick()
        {
            if (!_timeRater.Execute(Time.fixedDeltaTime)) return;

            CreateBullet();
            CustomExtentions.ColorLog($"Player Pos {_playerTransform.position}", Color.blue);
        }

        private void CreateBullet()
        {
            var bullet = ParticlePool.SharedInstance.GetPooledObject(_skillRarityData.bulletPref);
            bullet.transform.position = _playerTransform.position + Vector3.up;
            bullet.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-360, 360));
            bullet.SetActive(true);
        }
    }

    [Serializable]
    public struct AKWeaponSkillByRarity
    {
        public SkillRarity rarity;
        public GameObject bulletPref;
        public float fireRate;

    }
}