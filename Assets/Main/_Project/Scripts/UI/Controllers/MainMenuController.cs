using System;
using _Project.Scripts.Level;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{

    public class MainMenuController : IStartable
    {
        [Inject] private MainMenuControllerData _data;
        [Inject] private InLevelEvents _inLevelEvents;
        



        public void Start()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _data.playBtn.onClick.AddListener(() => PlayBtnClicked());
        }

        private void PlayBtnClicked()
        {
            //Animation and show panels;
            //Load Level 
            //Delay
            _inLevelEvents.onNextLevel?.Invoke();
        }
    }
    [Serializable]
    public class MainMenuControllerData
    {
        public Button playBtn;
    }


}