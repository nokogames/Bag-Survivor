



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Level;
using _Project.Scripts.SkillManagement.SO.Skills;
using JetBrains.Annotations;
using Pack.GameData;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace _Project.Scripts.UI.Controllers
{
    public class ActiveSkillUIController : IStartable, IPlayerUpgradedReciver, IFixedTickable
    {
        [Inject] private PlayerUpgradedData _playerUpgradedData;
        [Inject] private ActiveSkillUIControllerData _data;
        private Dictionary<SkillBase, ActiveSkillBehaviours> _activeSkillBehaviours;
        [Inject] private InLevelEvents _inLevelEvents;
        public void Start()
        {
            _activeSkillBehaviours = new();
            _playerUpgradedData.AddReciver(this);
            _inLevelEvents.onNextLevel += OnNextLevel;
        }

        private void OnNextLevel()
        {
            _activeSkillBehaviours.ForEachKey(x => RemoveActiveSkillUI(x));
        }

        public void ActivatedSkill(SkillBase skill)
        {
            CreateActiveSkillUI(skill);
        }

        public void DeactivatedSkill(SkillBase skill)
        {
            RemoveActiveSkillUI(skill);
        }

        private void RemoveActiveSkillUI(SkillBase skill)
        {
            var activeSkillBehaviours = _activeSkillBehaviours[skill];
            _activeSkillBehaviours.Remove(skill);
            GameObject.Destroy(activeSkillBehaviours.gameObject);
        }

        private void CreateActiveSkillUI(SkillBase skill)
        {
            var activeSkillUI = GameObject.Instantiate(_data.activeSkillUIPrefab, _data.activeSkillHolder);
            var activeSkillBehaviours = activeSkillUI.GetComponent<ActiveSkillBehaviours>();
            activeSkillBehaviours.Initialize(skill.Icon);
            _activeSkillBehaviours[skill] = activeSkillBehaviours;
        }
        public void OnUpgraded()
        {

        }

        public void FixedTick()
        {
            foreach (var kvp in _activeSkillBehaviours)
                kvp.Value.SetBar(kvp.Key.SkillPercentage);

        }
    }

    [Serializable]
    public class ActiveSkillUIControllerData
    {
        public GameObject activeSkillUIPrefab;
        public Transform activeSkillHolder;
    }
}