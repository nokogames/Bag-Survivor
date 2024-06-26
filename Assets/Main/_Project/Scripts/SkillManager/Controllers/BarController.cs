using System;



using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.UI.Controllers;

using Pack.GameData;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.SkillManagement.Controllers
{
    public class BarController : IStartable
    {
        [Inject] private PlayerInGameUpgradeBarController _barUiController;
        [Inject] private PlayerUpgradeDatabase _playerUpgradeDatabase;
        private RunTimePlayerData _playerRuntimeUpgradeData;
        private GameData _gameData;
        private SkillUIController _skillUIController;

        //[Inject] private SkillReciverController _skillReciever;
        private float _barProgressAmountPerXp;
        public void Start()
        {
            PrepareData();
            SetStartValuesToUi();
            _playerRuntimeUpgradeData = new();
            _playerRuntimeUpgradeData.Reset();
        }
        private void PrepareData()
        {
            var levelInfo = _playerUpgradeDatabase.levelInfos[_gameData.CurrentLvl];
            _barProgressAmountPerXp = 1f / levelInfo.XPList[_playerRuntimeUpgradeData.StartLvl];
        }
        private void SetStartValuesToUi()
        {
            _barUiController.SetCurrentUpgradeLvl(_playerRuntimeUpgradeData.StartLvl);
            _barUiController.SetNextUpgradeLvl(_playerRuntimeUpgradeData.NextLvl);
            _barUiController.SetBar(_playerRuntimeUpgradeData.BarFillAmount);
        }


        public void CollectedXp()
        {
            _playerRuntimeUpgradeData.BarFillAmount += _barProgressAmountPerXp;
            _barUiController.SetBar(_playerRuntimeUpgradeData.BarFillAmount);
            if (_playerRuntimeUpgradeData.BarFillAmount >= 1) AbleToUpgrade();
        }

        private void AbleToUpgrade()
        {
            _skillUIController.ShowPanel();

        }
        public void Upgraded()
        {

            _playerRuntimeUpgradeData.LevelUp();
            PrepareData();
            SetStartValuesToUi();

        }
    }
}