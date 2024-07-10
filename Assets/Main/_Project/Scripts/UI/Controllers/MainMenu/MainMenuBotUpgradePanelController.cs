using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using DG.Tweening;
using Pack.GameData;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers.MainMenu
{
    public class MainMenuBotUpgradePanelController : IStartable
    {
        [Inject] SavedPlayerData _savedPlayerData;
        [Inject] PlayerUpgradeDatabase _upgradeDatabase;
        [Inject] BotUpgradePanelData _panelData;
        [Inject] GameData _gameData;
        [Inject] InGamePanelController _inGamePanelController;
        private List<BotUpgradeUIBehaviour> _uiBehaviours;
        [Inject] private UpgradeVisualController _upgradeVisualController;
        public void Start()
        {
            _uiBehaviours = new(_upgradeDatabase.BotMainMenuUpgradeInfos.Count);
            CreateUpgradeItems();
        }

        private void CreateUpgradeItems()
        {
            var datas = _upgradeDatabase.BotMainMenuUpgradeInfos;
            foreach (var data in datas)
            {
                var createdUiBehaviour = GameObject.Instantiate(_panelData.upgradeItemPrefab, _panelData.parentObj).GetComponent<BotUpgradeUIBehaviour>();
                createdUiBehaviour.Initialize(data, _gameData.playerResource, this);
                _uiBehaviours.Add(createdUiBehaviour);

            }
            SortBehaviors();
        }

        private void SortBehaviors()
        {
            _uiBehaviours = _uiBehaviours.OrderBy(x => x.SortValue).ToList();
            for (int i = 0; i < _uiBehaviours.Count; i++)
            {
                _uiBehaviours[i].gameObject.transform.SetSiblingIndex(i);
            }
        }

        internal void UpgradeBtnClicked(PlayerMainMenuUpgradeInfo playerUpgradeInfo)
        {
            _gameData.playerResource.GemCount -= (int)playerUpgradeInfo.Price;
            playerUpgradeInfo.Upgraded(_savedPlayerData);
            _uiBehaviours.ForEach(x => x.MoneyChanged());
            SortBehaviors();
            _savedPlayerData.Upgraded();
            _inGamePanelController.SetGemCountTxt();
            _upgradeVisualController.BotUpgraded();
            _gameData.Save();

        }
        private Tween _openAnim;
        public void Enable(bool isActive)
        {
            if (_openAnim != null) _openAnim.Kill();
            _panelData.verticalLayoutGroup.spacing = 200f;
            _openAnim = DOTween.To(() => _panelData.verticalLayoutGroup.spacing, x => _panelData.verticalLayoutGroup.spacing = x, 0, 0.5f).SetEase(Ease.OutBack);
            _panelData.panel.SetActive(isActive);
        }
    }

    [Serializable]
    public class BotUpgradePanelData
    {
        public GameObject upgradeItemPrefab;
        public Transform parentObj;
        public GameObject panel;
        public VerticalLayoutGroup verticalLayoutGroup;

    }




}