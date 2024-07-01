using System;
using _Project.Scripts.Level;
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

        public void Start()
        {
            _panelControllerData.Initialize();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _panelControllerData.nextSectionBtn.onClick.AddListener(() => _inLevelEvents.onNextSection?.Invoke());
            _panelControllerData.goMainMenuBtn.onClick.AddListener(() => OpenMainMenu());
            _inLevelEvents.onShowNextSectionUI += ShowNextSectionPanel;
            _inLevelEvents.onNextSection += NextSection;
            _inLevelEvents.onNextLevel += NextLevel;

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
            _panelControllerData.OpenPanel(_panelControllerData.NextLevelPanel);
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
        public Button nextSectionBtn;
        public GameObject diedPanel;
        public GameObject mainPanel;
        public Button goMainMenuBtn;
        internal void Initialize()
        {
            OpenPanel(InGamePanel);
        }

        internal void OpenPanel(GameObject nextLevelPanel)
        {
            CloseAll();
            nextLevelPanel.SetActive(true);
        }

        private void CloseAll()
        {
            InGamePanel.SetActive(false);
            NextLevelPanel.SetActive(false);
            diedPanel.SetActive(false);
            mainPanel.SetActive(false);
        }
    }
}