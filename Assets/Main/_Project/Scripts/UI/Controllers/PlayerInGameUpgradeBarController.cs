using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.UI.Controllers
{

    public class PlayerInGameUpgradeBarController
    {
        [Inject] PlayerInGameUpgradeBarControllerData data;
        
        [Inject] 
        public PlayerInGameUpgradeBarController()
        {
          
        }
        public void SetCurrentUpgradeLvl(int lvl)
        {
            data.currentUpgradeLvl.text = lvl.ToString();
        }
        public void SetNextUpgradeLvl(int lvl)
        {
            data.nextUpgradeLvl.text = lvl.ToString();

        }

    }

    [System.Serializable]
    public class PlayerInGameUpgradeBarControllerData
    {
        public Image bar;
        public TextMeshProUGUI currentUpgradeLvl;
        public TextMeshProUGUI nextUpgradeLvl;
    }
}