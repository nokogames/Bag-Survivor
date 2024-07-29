using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Bot.Gun;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.SkillManagement.SO.Skills;
using Pack.GameData;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace _Project.Scripts.Character.Bot.Controllers
{
    public class BotUpgradeApplyer : IPlayerUpgradedReciver, IStartable, IDisposable
    {
        [Inject] private SavedPlayerData _playerSavedData;
        [Inject] private BotAgentController _agentController;
        [Inject] private UpgradedBotGun _botGun;
        public void Start()
        {
            _playerSavedData.AddReciver(this);
            SetBotSpeed();
            SetBotFireRate();
            SetBotDamage();
        }
        public void OnUpgraded()
        {
            SetBotSpeed();
            SetBotFireRate();
            SetBotDamage();
        }

        private void SetBotSpeed()
        {
            _agentController.SetSpeed(_playerSavedData.botSpeed);
        }


        private void SetBotFireRate()
        {
            _botGun.SetFireRate(_playerSavedData.botFirerate);
        }

        private void SetBotDamage()
        {
            _botGun.SetDamage(_playerSavedData.botDamage);
        }


        public void Dispose()
        {
            _playerSavedData.RemoveReciver(this);
        }

        public void ActivatedSkill(SkillBase skill)
        {  
             Debug.Log("3 XX");
            //  $"ActivatedSkill {skill}".ColorLog(Color.green);
        }

        public void DeactivatedSkill(SkillBase skill)
        {
            //  $"DeactivatedSkill{skill}".ColorLog(Color.red);
        }
    }
}