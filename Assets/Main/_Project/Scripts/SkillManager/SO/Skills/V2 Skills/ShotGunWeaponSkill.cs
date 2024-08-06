





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "ShotGunWeaponSkill", menuName = "ScriptableObjects/SkillSystem/ShotGunWeaponSkill", order = 0)]
    public class ShotGunWeaponSkill : SkillBase
    {

        [SerializeField] private List<ShotGunWeaponSkillByRarity> _akWeaponByRarity;
        [SerializeField] private GameObject bulletPrefab;
        private ShotGunWeaponSkillByRarity _skillRarityData;
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

            CreateBullet(_skillRarityData.count);
            CustomExtentions.ColorLog($"Player Pos {_playerTransform.position}", Color.blue);
        }

        private float _randomAngle = 10;
        private void CreateBullet(int count)
        {
            // var randomRotate = Vector3.up * UnityEngine.Random.Range(-360, 360);
            for (int i = 0; i < count; i++)
            {

                var bullet = ParticlePool.SharedInstance.GetPooledObject(bulletPrefab);
                bullet.transform.position = _playerTransform.position + Vector3.up;
                // bullet.transform.Rotate(randomRotate);
                bullet.transform.forward = _playerTransform.forward;
                bullet.transform.Rotate(bullet.transform.up * UnityEngine.Random.Range(-_randomAngle, _randomAngle));
                bullet.transform.Rotate(bullet.transform.right * UnityEngine.Random.Range(-_randomAngle, _randomAngle));
                bullet.SetActive(true);
            }
        }
    }

    [Serializable]
    public struct ShotGunWeaponSkillByRarity
    {
        public SkillRarity rarity;
        public float fireRate;
        public int count;

    }
}