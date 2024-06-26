using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI.Interfacies;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{
    public class SkillUIController : IStartable
    {
        [Inject] private SkillUIControllerData _skillUIControllerData;
        [Inject] private InputDataSO _inputDataSO;
        [Inject] private UIMediatorEventHandler _eventHandler;

        public Transform SkillTransformParent => _skillUIControllerData.skillParentTransform;
        public List<SkillBehaviour> SkillBehaviors => _skillUIControllerData.skillBehaviors;
        public Button RerollBtn => _skillUIControllerData.RerollBtn;
        public void Start()
        {
            //_eventHandler.AddReciever(this);
            _skillUIControllerData.RerollBtn.onClick.AddListener(_eventHandler.RerollBtnClicked);
            for (int i = 0; i < _skillUIControllerData.skillBehaviors.Count; i++) _skillUIControllerData.skillBehaviors[i].Initialize(_eventHandler);
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





        // //Test
        // public void CloseBtnClicked()
        // {
        //     HidePanel();
        // }

        // public void OnSkillBtnClicked(SkillBase skill)
        // {
        //     // throw new NotImplementedException();
        // }
    }



    [Serializable]
    public class SkillUIControllerData
    {
        public GameObject SkillPanel;
        public Transform skillParentTransform;
        public List<SkillBehaviour> skillBehaviors;
        public Button RerollBtn;
    }
}