using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.UI.Interfacies;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{
    public class SkillUIController : IStartable, ISkillReciever
    {
        [Inject] private SillUIControllerData _skillUIControllerData;
        [Inject] private InputDataSO _inputDataSO;
        [Inject] private UIMediatorEventHandler _eventHandler;
        public void Start()
        {
            _eventHandler.AddReciever(this);
            Setup();
        }

        private void Setup()
        {
            HidePanel();
        }

        public void ShowPanel()
        {
            _skillUIControllerData.SkillPanel.SetActive(true);
            _inputDataSO.disableJoystick = true;
            Time.timeScale = 0;
        }
        public void HidePanel()
        {
            _skillUIControllerData.SkillPanel.SetActive(false);
            _inputDataSO.disableJoystick = false;
            Time.timeScale = 1;

        }

        public void CloseBtnClicked()
        {
            HidePanel();
        }
    }



    [Serializable]
    public class SillUIControllerData
    {
        public GameObject SkillPanel;
    }
}