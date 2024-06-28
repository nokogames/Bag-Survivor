using System;
using System.Collections.Generic;
using System.Linq;

using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;



namespace _Project.Scripts.UI.Controllers
{


    public class SectionUIController
    {
        [Inject] private SectionUIControllerData _sectionUIControllerData;

        public void SetLevelTxt(int level)
        {
            _sectionUIControllerData.LevelTxt.text = level.ToString();
        }
        public void SetSectionTxt(int section)
        {
            _sectionUIControllerData.SectionTxt.text = section.ToString();
        }
    }

    [Serializable]
    public class SectionUIControllerData
    {
        public TextMeshProUGUI LevelTxt;
        public TextMeshProUGUI SectionTxt;

    }
}