using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Datas.SO;
using Pack.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Controllers.MainMenu
{

    public class PlayerUpgradeUIBehaviour : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Button _upgradeButton;

        private Image _bgImg;
        private PlayerMainMenuUpgradeInfo _playerUpgradeInfo;
        private PlayerResource _playerResource;
        private MainMenuPlayerUpgradePanelController _controller;
        private void Awake()
        {
            _bgImg = GetComponent<Image>();
            _upgradeButton.onClick.AddListener(UpgradeBtnClicked);
        }
        public void Initialize(PlayerMainMenuUpgradeInfo upgradeInfo, PlayerResource playerResource, MainMenuPlayerUpgradePanelController controller)
        {
            _controller = controller;
            _playerResource = playerResource;
            _playerUpgradeInfo = upgradeInfo;
            _bgImg.sprite = upgradeInfo.BgSprite;
            SetupView();
        }

        private void SetupView()
        {
            var price = _playerUpgradeInfo.Price;
            if (price < 0)
            {
                //<color=green>5</color>
                priceText.text = "Max";
                infoText.text = $"Upgrade Completed <color=green>{_playerUpgradeInfo.Value}'</color> ";
                _upgradeButton.interactable = false;
                return;
            }
            priceText.text = _playerUpgradeInfo.Price.ToString("f0");
            _upgradeButton.interactable = _playerResource.CoinCount >= price;
            infoText.text = $"{_playerUpgradeInfo.UpgradeInfo} <color=green>{_playerUpgradeInfo.Value}'</color> to <color=red>{_playerUpgradeInfo.NextValue} ";

        }

        public void MoneyChanged()
        {
            SetupView();
        }

        public void UpgradeBtnClicked()
        {


            _controller.UpgradeBtnClicked(_playerUpgradeInfo);
            _playerResource.CoinCount -= (int)_playerUpgradeInfo.Price;

            // _controller.Upgraded();
        }

        public int SortValue
        {
            get
            {
                return _playerUpgradeInfo.Price < 0 ? 999999 : _playerUpgradeInfo.Price;
            }
        }
    }

}