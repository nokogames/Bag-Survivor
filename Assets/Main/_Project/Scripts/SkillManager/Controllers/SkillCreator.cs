using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.SO;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.SkillManagement.Controllers
{
    public class SkillCreator : IStartable
    {
        [Inject] private List<SkillBase> Skills;
        [Inject] private SkillUIController _skillUIController;
        [Inject] private PlayerUpgradedData _playerUpgradedData;

        private List<SkillBehaviour> _skillBehaviors = new();

        private readonly static int SKILL_PREFAB_POOL_INDEX = 4;

        public void Start()
        {
            PrepareSkillBehaviors();
            CreateSkill(3);
        }

        private void PrepareSkillBehaviors()
        {

            _skillBehaviors = _skillUIController.SkillBehaviors;
            Skills.ForEach(skill => skill.Initialize(_playerUpgradedData));

        }


        private void CreateSkill(int skillCount = 3)
        {
            int[] skillIndexs = new int[] { -1, -1, -1 };
            for (int i = 0; i < skillCount; i++)
            {
                //UnityEngine.Random.InitState(i);
                int randomIndex = UnityEngine.Random.Range(0, Skills.Count);
                SkillBase rondomSkill = Skills[randomIndex];
                while (skillIndexs.Contains(randomIndex) || _playerUpgradedData.IsActiveSkill(rondomSkill))
                {
                    randomIndex = UnityEngine.Random.Range(0, Skills.Count);
                    rondomSkill = Skills[randomIndex];
                }
                skillIndexs[i] = randomIndex;

                //    rondomSkill = Skills[randomIndex];
                SkillRarity skillRarity = rondomSkill.GetRandomRarity();

                SkillBehaviour skillBehaviour = _skillBehaviors[i];

                skillBehaviour.Setup(new CreatedSkillInfo(skillRarity, rondomSkill));

            }

        }

        internal void Reroll()
        {
            CreateSkill();
        }

        internal void ReCreateSkill()
        {
            CreateSkill();
        }

        internal void DisableMoveSkillVisual()
        {
            _skillBehaviors.ForEach(x => x.DisableMoveSkillVisual());
        }
    }


    public class CreatedSkillInfo
    {
        private SkillRarity _skillRarity;
        private SkillBase _skillBase;
        public SkillRarity SkillRarity => _skillRarity;
        public SkillBase Skill => _skillBase;
        public CreatedSkillInfo(SkillRarity skillRarity, SkillBase skillBase)
        {
            _skillBase = skillBase;
            _skillRarity = skillRarity;
        }
    }
}