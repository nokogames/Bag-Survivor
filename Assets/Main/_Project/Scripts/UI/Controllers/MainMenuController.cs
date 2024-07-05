using System;
using System.Collections.Generic;
using _Project.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{

    public class MainMenuController : IStartable, ITickable
    {
        [Inject] private MainMenuView _data;
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

        public void Tick()
        {
        }
    }
    [Serializable]
    public class MainMenuView
    {
        public Button playBtn;
        public List<GameObject> pages;
        public List<MenuBtnBehaviour> menuBtnBehaviours;
        public Scrollbar mainScrollBar;


        [Header("Play Page")]
        public List<MapBehaviour> mapBehaviours;
        public Scrollbar mapScrollBar;
        public Button leftBtn;
        public Button rightBtn;
    }


}