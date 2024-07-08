using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers.MainMenu
{
    public class MainMenuPlayerUpgradePanelController : IStartable
    {
        [Inject] SavedPlayerData _savedPlayerData;
        [Inject] PlayerUpgradeDatabase _upgradeDatabase;
        [Inject] PlayerUpgradePanelData _panelData;
        [Inject] GameData _gameData;
        private List<PlayerUpgradeUIBehaviour> _uiBehaviours;
        public void Start()
        {
            _uiBehaviours = new(_upgradeDatabase.PlayerMainMenuUpgradeInfos.Count);
            CreateUpgradeItems();
        }

        private void CreateUpgradeItems()
        {
            var datas = _upgradeDatabase.PlayerMainMenuUpgradeInfos;
            foreach (var data in datas)
            {
                var createdUiBehaviour = GameObject.Instantiate(_panelData.upgradeItemPrefab, _panelData.parentObj).GetComponent<PlayerUpgradeUIBehaviour>();
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
            _gameData.playerResource.CoinCount -= (int)playerUpgradeInfo.Price;
            playerUpgradeInfo.Upgraded(_savedPlayerData);
            _uiBehaviours.ForEach(x => x.MoneyChanged());
            SortBehaviors();
            _savedPlayerData.Upgraded();

        }
    }

    [Serializable]
    public class PlayerUpgradePanelData
    {
        public GameObject upgradeItemPrefab;
        public Transform parentObj;

    }




}