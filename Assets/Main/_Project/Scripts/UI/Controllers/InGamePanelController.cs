using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Pack.GameData;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace _Project.Scripts.UI.Controllers
{
    public class InGamePanelController : IStartable
    {
        [Inject] private InGamePanelView _view;
        [Inject] private GameData _gameData;

        public void Start()
        {
            SetCoinCounTxt();
            SetGemCountTxt();
        }

        public string Timer
        {
            set
            {
                _view.timer.text = value;
            }
        }

        public void SetCoinCounTxt(int coin)
        {
            _view.coinTxt.text = coin.ToString();
        }
        public void SetCoinCounTxt()
        {
            _view.coinTxt.text = _gameData.playerResource.CoinCount.ToString();
        }
        public void SetGemCountTxt()
        {
            _view.gemTxt.text = _gameData.playerResource.GemCount.ToString();
        }
    }

    [Serializable]
    public class InGamePanelView
    {
        public TextMeshProUGUI timer;
        public TextMeshProUGUI coinTxt;
        public TextMeshProUGUI gemTxt;
    }
}