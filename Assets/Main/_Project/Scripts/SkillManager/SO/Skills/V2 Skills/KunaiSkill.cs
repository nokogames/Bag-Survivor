






using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "KunaiSkill", menuName = "ScriptableObjects/SkillSystem/KunaiSkill", order = 0)]
    public class KunaiSkill : SkillBase
    {

        [SerializeField] private List<KunaiSkillByRarity> _axeDataByRarity;
        private KunaiSkillByRarity _skillRarityData;


        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = _axeDataByRarity.First(x => x.rarity == rarity);
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
                _skillRarityData = _axeDataByRarity.First(x => x.rarity == skillRarity);
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
        public override float SkillPercentage => _crrTime / _fireRate;
        internal override void AttactFixedTick()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _fireRate) return;
            _crrTime = 0;
            CreateAxe(_skillRarityData.count);
            CustomExtentions.ColorLog($"Player Pos {_playerTransform.position}", Color.blue);
        }

        private void CreateAxe(int count = 1)
        {
            float anglePerBullet = 360 / count;
            float crrAngle = 0;
            for (int i = 0; i < count; i++)
            {
                var bullet = ParticlePool.SharedInstance.GetPooledObject(_skillRarityData.prefab);
                bullet.transform.position = _playerTransform.position + Vector3.up;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 0);

                bullet.transform.Rotate(Vector3.up * crrAngle);
                crrAngle += anglePerBullet;
                bullet.SetActive(true);
            }
        }
    }

    [Serializable]
    public struct KunaiSkillByRarity
    {
        public SkillRarity rarity;
        public GameObject prefab;
        public float fireRate;
        public int count;

    }
}