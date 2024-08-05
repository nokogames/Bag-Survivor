
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.Controllers;
using UnityEngine;


namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "HealtPodSkill", menuName = "ScriptableObjects/SkillSystem/HealtPodSkill", order = 0)]
    public class HealtPodSkill : SkillBase
    {
        [SerializeField] private List<HealtPodByRarity> healtPodByRarities;
        [SerializeField] private GameObject healingParticle;
        private HealtPodByRarity _skillRarityData;

        private TimeRater _timeRater;
        public override void Initialize(PlayerUpgradedData playerUpgradedData)
        {
            base.Initialize(playerUpgradedData);
            _timeRater = TimeRater.Init(100);

        }

        public override string GetInfoTxt(SkillRarity rarity)
        {
            var result = healtPodByRarities.First(x => x.rarity == rarity);
            return $"{InfoTxt} +{_skillRarityData.healtAmount}";
        }


        public override bool ActiveSkill(Transform playerTransform, SkillRarity skillRarity)
        {

            _playerTransform = playerTransform;



            var result = _playerUpgradedData.ActiveSkill(this);
            if (result)
            {
                _skillRarityData = healtPodByRarities.First(x => x.rarity == skillRarity);
                //  _fireRate = _skillRarityData.ballFireRate;
                _timeRater.SetTimeRate(_skillRarityData.healtTimeRate);
            }
            return result;
        }


        internal override void AlwaysFixedTick()
        {   
            CustomExtentions.ColorLog($"{_timeRater.CrrTime}",Color.blue);
            if (!_timeRater.Execute(Time.fixedDeltaTime)) return;
            Healing();


        }



        public override bool DeactivateSkill()
        {
            Debug.Log("Deactivated");
            return _playerUpgradedData.DeactiveSkill(this);
        }
        private void Healing()
        {
            _playerUpgradedData.additionalHelat = _skillRarityData.healtAmount;
            _playerUpgradedData.Upgraded();
            var particle = ParticlePool.SharedInstance.GetPooledObject(healingParticle);
            particle.transform.position = _playerTransform.position;
            particle.gameObject.SetActive(true);

        }


    }

    [Serializable]
    public struct HealtPodByRarity
    {
        public SkillRarity rarity;
        public int healtTimeRate;
        public float healtAmount;
    }
}