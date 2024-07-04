using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{
    public class LevelEndDataPanel : IStartable
    {
        [Inject] private LevelEndDataPanelView panelData;

        public void Start()
        {
        }

        public void SetPlayTime(string time)
        {
            panelData.PlayTime = time;
        }

        internal void SetPercentage(float percentage)
        {
            panelData.ClearPercentage = percentage.ToString("f1");
        }

        internal void SetCollectedGem(string v)
        {
            panelData.CollectedGem = v;
        }
        internal void SetCollectedCoinTxt(string v)
        {
              panelData.EarnedCoin=v;
        }
    }

    [Serializable]
    public class LevelEndDataPanelView
    {
        [SerializeField] private List<TextMeshProUGUI> CollectedGemTxts;
        public string CollectedGem
        {
            set => CollectedGemTxts.ForEach(x => x.text = value);

        }
        [SerializeField] private List<TextMeshProUGUI> PlayTimeTxt;
        public string PlayTime
        {
            set => PlayTimeTxt.ForEach(x => x.text = $"You Survivied {value} min!");

        }
        [SerializeField] private List<TextMeshProUGUI> ClearPercentageTxts;

        public string ClearPercentage { set => ClearPercentageTxts.ForEach(x => x.text = $"Stage Cleared {value}%"); }

        [SerializeField] private List<TextMeshProUGUI> EarnedCoinTxts;
        public string EarnedCoin { set => EarnedCoinTxts.ForEach(x => x.text = value); }

    }
}