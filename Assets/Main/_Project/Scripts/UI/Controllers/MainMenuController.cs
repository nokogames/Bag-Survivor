using System;
using System.Collections.Generic;
using _Project.Scripts.Level;
using _Project.Scripts.Loader;
using _Project.Scripts.UI.Controllers.MainMenu;
using Pack.GameData;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{

    public class MainMenuController : IStartable, ITickable
    {
        [Inject] private MainMenuView _data;
        [Inject] private InLevelEvents _inLevelEvents;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private MainMenuPlayerUpgradePanelController _playerUpgradePanelController;
        [Inject] private MainMenuBotUpgradePanelController _botUpgradePanelController;

        [Inject] private UpgradeVisualController _upgradeVisualController;
        [Inject] private GameData _gameData;

        private int _targetLvl;
        public void Start()
        {
            PrepareData();
            RegisterEvents();
            OpenPlayerUpgradeBtnClicked();
        }

        private void PrepareData()
        {
            _data.pImg = _data.playerUpgradePanelBtn.GetComponent<Image>();
            _data.bImg = _data.botUpgradePanelBtn.GetComponent<Image>();
        }

        private void RegisterEvents()
        {
            _data.playBtn.onClick.AddListener(() => PlayBtnClicked());
            _data.playerUpgradePanelBtn.onClick.AddListener(() => OpenPlayerUpgradeBtnClicked());
            _data.botUpgradePanelBtn.onClick.AddListener(() => OpenBotUpgradeBtnClicked());
        }

        private void PlayBtnClicked()
        {
            //Animation and show panels;
            //Load Level 
            //Delay
            if (_targetLvl > 2) _targetLvl = 1;
            string targetLvlName = $"Level{_targetLvl + 1}";

            _sceneLoader.LoadLevelWithSplash(targetLvlName, () =>
            {
                _gameData.CurrentLvl = _targetLvl;
                _gameData.GetSavedLevelData().IsOpen = true;
                _gameData.Save();
                _inLevelEvents.onNextLevel?.Invoke();
            }
            );

        }
        private void OpenPlayerUpgradeBtnClicked()
        {
            _playerUpgradePanelController.Enable(true);
            _botUpgradePanelController.Enable(false);
            _data.pImg.sprite = _data.pEnableSprite;
            _data.bImg.sprite = _data.bDisableSprite;
            _data.bImg.SetNativeSize();
            _data.pImg.SetNativeSize();
            _upgradeVisualController.PlayerSelected();
        }
        private void OpenBotUpgradeBtnClicked()
        {
            _playerUpgradePanelController.Enable(false);
            _botUpgradePanelController.Enable(true);
            _data.pImg.sprite = _data.pDisableSprite;
            _data.bImg.sprite = _data.bEnableSprite;
            _data.bImg.SetNativeSize();
            _data.pImg.SetNativeSize();
            _upgradeVisualController.BotSelected();
        }
        public void SetPlayBtnInteractable(bool interactable)
        {
            _data.playBtn.interactable = interactable;
        }
        public void SetTargetLvl(int lvl, bool interactableBtn = false)
        {

            _targetLvl = lvl;
            SetPlayBtnInteractable(interactableBtn);
        }
        public void Tick()
        {
        }
    }
    [Serializable]
    public class MainMenuView
    {
        public Button playBtn;
        public List<GameObject> pages;
        public List<MenuBtnBehaviour> menuBtnBehaviours;
        public Scrollbar mainScrollBar;


        [Header("Play Page")]
        // public List<MapBehaviour> mapBehaviours;
        public Scrollbar mapScrollBar;
        public Button leftBtn;
        public Button rightBtn;
        [Header("Player Upgrade Page")]
        public Button playerUpgradePanelBtn;
        public Image pImg;
        public Sprite pEnableSprite;
        public Sprite pDisableSprite;
        public Button botUpgradePanelBtn;
        public Image bImg;
        public Sprite bEnableSprite;
        public Sprite bDisableSprite;

    }


}