using System;
using _Project.Scripts.Level;
using _Project.Scripts.Loader;
using Pack.GameData;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{
    public class PanelController : IStartable
    {
        [Inject] private PanelControllerData _panelControllerData;
        [Inject] private InLevelEvents _inLevelEvents;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameData _gameData;

        public void Start()
        {
            _panelControllerData.Initialize();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _panelControllerData.nextSectionBtn.onClick.AddListener(() => _inLevelEvents.onNextSection?.Invoke());
            _panelControllerData.goMainMenuBtn.onClick.AddListener(() => OpenMainMenu());
            _panelControllerData.nextLevelBtn.onClick.AddListener(() => LoadNextLvl());
            _inLevelEvents.onShowNextSectionUI += ShowNextSectionPanel;
            _inLevelEvents.onNextSection += NextSection;
            _inLevelEvents.onNextLevel += NextLevel;
            _inLevelEvents.onShowNextLevelUI += ShowNextLevelPanel;


        }

        private void LoadNextLvl()
        {
            _gameData.CurrentLvl++;

            _sceneLoader.LoadLevelWithSplash("Level" + (_gameData.CurrentLvl + 1).ToString(), _inLevelEvents.onNextLevel);

        }

        private void ShowNextLevelPanel()
        {
            _panelControllerData.OpenPanel(_panelControllerData.NextLevelPanel);
        }
        private void NextLevel()
        {
            _panelControllerData.OpenPanel(_panelControllerData.InGamePanel);
        }

        public void PlayerDied()
        {
            _panelControllerData.OpenPanel(_panelControllerData.diedPanel);
        }
        private void NextSection()
        {
            _panelControllerData.OpenPanel(_panelControllerData.InGamePanel);
        }

        private void ShowNextSectionPanel()
        {
            _panelControllerData.OpenPanel(_panelControllerData.NextSectionPanel);
        }
        //From button
        public void OpenMainMenu()
        {
            _panelControllerData.OpenPanel(_panelControllerData.mainPanel);
        }
    }

    [Serializable]
    public class PanelControllerData
    {
        public GameObject InGamePanel;
        public GameObject NextLevelPanel;
        public GameObject NextSectionPanel;
        public Button nextSectionBtn;
        public Button nextLevelBtn;
        public GameObject diedPanel;
        public GameObject mainPanel;
        public Button goMainMenuBtn;
        internal void Initialize()
        {
            OpenPanel(InGamePanel);
        }

        internal void OpenPanel(GameObject panel)
        {
            CloseAll();
            panel.SetActive(true);
        }

        private void CloseAll()
        {
            InGamePanel.SetActive(false);
            NextSectionPanel.SetActive(false);
            NextLevelPanel.SetActive(false);
            diedPanel.SetActive(false);
            mainPanel.SetActive(false);
        }
    }
}