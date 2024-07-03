using System;



using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Level;
using _Project.Scripts.UI.Controllers;

using Pack.GameData;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.SkillManagement.Controllers
{
    public class BarController : IStartable, IDisposable
    {
        [Inject] private PlayerInGameUpgradeBarController _barUiController;
        [Inject] private PlayerUpgradeDatabase _playerUpgradeDatabase;
        [Inject] private BarData _barData;
        [Inject] private GameData _gameData;
        [Inject] private SkillUIController _skillUIController;
        [Inject] private SkillCreator _skillCreator;
        [Inject] private InLevelEvents _inLevelEvents;
        //[Inject] private SkillReciverController _skillReciever;
        private float _barProgressAmountPerXp;
        public void Start()
        {
            // _playerRuntimeUpgradeData = new();
            _inLevelEvents.onNextSection += NextSection;
            _inLevelEvents.onNextLevel += NextLevel;
            SetupStart();
        }

        private void NextLevel()
        {
            SetupStart();
        }

        private void NextSection()
        {
            SetupStart();

        }

        private void SetupStart()
        {
            _barData.Reset();
            PrepareData();
            SetStartValuesToUi();
        }

        private void PrepareData()
        {
            var levelInfo = _playerUpgradeDatabase.levelInfos[_gameData.CurrentLvl];
            _barProgressAmountPerXp = 1f / levelInfo.XPList[_barData.StartLvl];
        }
        private void SetStartValuesToUi()
        {
            _barUiController.SetCurrentUpgradeLvl(_barData.StartLvl);
            _barUiController.SetNextUpgradeLvl(_barData.NextLvl);
            _barUiController.SetBar(_barData.BarFillAmount);
        }


        public void CollectedXp()
        {
            _barData.BarFillAmount += _barProgressAmountPerXp;
            _barUiController.SetBar(_barData.BarFillAmount);
            if (_barData.BarFillAmount >= 1) AbleToUpgrade();
        }

        private void AbleToUpgrade()
        {
            _skillCreator.ReCreateSkill();
            _skillUIController.ShowPanel();

        }
        public void Upgraded()
        {

            _barData.LevelUp();
            PrepareData();
            SetStartValuesToUi();

        }

        public void Dispose()
        {
            _inLevelEvents.onNextSection -= NextSection;
            _inLevelEvents.onNextLevel -= NextLevel;
        }
    }
}