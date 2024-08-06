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
    [CreateAssetMenu(fileName = "HammerSkill", menuName = "ScriptableObjects/SkillSystem/HammerSkill", order = 0)]
    public class HammerSkill : SkillBase
    {

        [SerializeField] private List<HummerSkillByRarity> _hummerDataByRarity;
        private HummerSkillByRarity _skillRarityData;
        private TimeRater _timeRater;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            _timeRater = TimeRater.Init(100);
        }
        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = _hummerDataByRarity.First(x => x.rarity == rarity);
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
                _skillRarityData = _hummerDataByRarity.First(x => x.rarity == skillRarity);
                _timeRater.SetTimeRate(_skillRarityData.fireRate);
                // _fireRate = _skillRarityData.fireRate;
            }
            return result;


        }
        public override bool DeactivateSkill()
        {
            Debug.Log("Deactivated");
            return _playerUpgradedData.DeactiveSkill(this);
        }

        // private float _fireRate;
        // private float _crrTime = 0;
        public override float SkillPercentage => _timeRater.Percentage;
        internal override void AttactFixedTick()
        {
            if (!_timeRater.Execute(Time.fixedDeltaTime)) return;

            CreateHummer(_skillRarityData.count).Forget();
            CustomExtentions.ColorLog($"Player Pos {_playerTransform.position}", Color.blue);
        }

        private async UniTaskVoid CreateHummer(int count = 1)
        {
            for (int i = 0; i < count;i++)
            {

                var bullet = ParticlePool.SharedInstance.GetPooledObject(_skillRarityData.hummerPrefab);
                bullet.transform.position = _playerTransform.position + Vector3.up;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                bullet.transform.Rotate(Vector3.up * UnityEngine.Random.Range(-360, 360));
                bullet.SetActive(true);
                await UniTask.Yield();

            }
        }
    }

    [Serializable]
    public struct HummerSkillByRarity
    {
        public SkillRarity rarity;
        public GameObject hummerPrefab;
        public float fireRate;
        public int count;

    }
}