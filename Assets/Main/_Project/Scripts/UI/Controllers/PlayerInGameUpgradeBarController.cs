using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using Pack.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{

    public class PlayerInGameUpgradeBarController : IStartable
    {
        [Inject] private PlayerInGameUpgradeBarControllerData _data;
        [Inject] private PlayerInGameUpgradeData _playerInGameUpgradeData;
        [Inject] private GameData _gameData;
        [Inject] private PlayerUpgradeDatabase _playerUpgradeDatabase;
        [Inject] private SkillUIController _skillUIController;
        private float _barProgressAmountPerXp;

        public void Start()
        {
            PrepareData();
            SetStartValuesToUi();
        }

        private void PrepareData()
        {
            var levelInfo = _playerUpgradeDatabase.levelInfos[_gameData.CurrentLvl];
            _barProgressAmountPerXp = 1f / levelInfo.XPList[_playerInGameUpgradeData.StartLvl];
        }

        private void SetStartValuesToUi()
        {
            SetCurrentUpgradeLvl(_playerInGameUpgradeData.StartLvl);
            SetNextUpgradeLvl(_playerInGameUpgradeData.NextLvl);
            SetBar(_playerInGameUpgradeData.BarFillAmount);
        }

        private void SetBar(float barFillAmount)
        {
            _data.bar.fillAmount = barFillAmount;
        }

        public void SetCurrentUpgradeLvl(int lvl)
        {
            _data.currentUpgradeLvl.text = lvl.ToString();
        }
        public void SetNextUpgradeLvl(int lvl)
        {
            _data.nextUpgradeLvl.text = lvl.ToString();

        }

        public void CollectedXp()
        {
            _playerInGameUpgradeData.BarFillAmount += _barProgressAmountPerXp;
            SetBar(_playerInGameUpgradeData.BarFillAmount);
            if (_playerInGameUpgradeData.BarFillAmount >= 1) AbleToUpgrade();
        }

        private void AbleToUpgrade()
        {
            _skillUIController.ShowPanel();
            //Show Skill panel

        }
    }

    [System.Serializable]
    public class PlayerInGameUpgradeBarControllerData
    {
        public Image bar;
        public TextMeshProUGUI currentUpgradeLvl;
        public TextMeshProUGUI nextUpgradeLvl;
    }
}