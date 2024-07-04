using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.EnemyRuntime;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;
/*
1-played time 
2-collected gem
3-earned money 
3-cleared percentage

Save level data ----


*/
namespace _Project.Scripts.Level
{
    public class LevelDataManager : IStartable, IFixedTickable
    {
        [Inject] private InLevelEvents _events;
        [Inject] private UIMediator _uiMediator;
        [Inject] private GameData _gameData;

        private InGamePanelController _inGamePanelController;
        private LevelEndDataPanel _levelEndDataPanel;

        private int _startGemCount;
        private int _collectedGemCount;
        private float _startTime;

        private float _playTime;
        public EnemyManager EnemyManager { get; set; }
        public void Start()
        {
            _events.onNextLevel += NextLevelStarted;
            _events.onShowNextLevelUI += OnShowNextLevelUI;
            _events.onShowNextSectionUI += OnShowNextSectionUI;


            _inGamePanelController = _uiMediator.InGamePanelController;
            _levelEndDataPanel = _uiMediator.LevelEndDataPanel;
        }

        private void NextLevelStarted()
        {
            _startTime = Time.time;
            _startGemCount = _gameData.playerResource.GemCount;
            _collectedGemCount = 0;
        }
        private void OnShowNextSectionUI()
        {
            SetPlayedTime();
            SetPercentage();
            SetCollectedGem();
            SetEarnedCoin();
        }



        private void OnShowNextLevelUI()
        {
            SetPlayedTime();
            SetPercentage();
            SetCollectedGem();
            SetEarnedCoin();
        }


        public void PlayerDied()
        {
            SetPlayedTime();
            SetPercentage();
            SetCollectedGem();
            SetEarnedCoin();
        }

        private void SetEarnedCoin()
        {
            int CoinCount = (int)EnemyManager.EarnedCoin();
            _gameData.playerResource.CoinCount += CoinCount;
            _inGamePanelController.SetCoinCounTxt();
            _levelEndDataPanel.SetCollectedCoinTxt(CoinCount.ToString());
        }

        private void SetCollectedGem()
        {
            _collectedGemCount = _gameData.playerResource.GemCount - _startGemCount;
            _levelEndDataPanel.SetCollectedGem(_collectedGemCount.ToString());
        }

        private void SetPercentage()
        {
            _levelEndDataPanel.SetPercentage(EnemyManager.ClearedPercentage());

        }
        private void SetPlayedTime()
        {
            _playTime = Time.time - _startTime;

            _levelEndDataPanel.SetPlayTime(_playTime.FormatTime());
        }

        public void FixedTick()
        {
            //Update play time ui txt

            _playTime = Time.time - _startTime;
            _inGamePanelController.Timer = _playTime.FormatTime();
            //  Debug.Log("Play time=" + _playTime.FormatTime());
        }




    }
}