

using System;
using System.Collections.Generic;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers.MainMenu
{

    public class MapPanelController : IStartable
    {
        [Inject] private MapPanelData _data;
        [Inject] private GameData _gameData;
        [Inject] private MainMenuController _mainMenuController;

        public List<MapBehaviour> mapBehaviours = new();
        public void Start()
        {
            Create();
        }

        private void Create()
        {
            for (int i = 0; i < _data.mapUiInfo.Count; i++)
            {
                var mapBehaviour = GameObject.Instantiate(_data.mapUIPrefab, _data.parentObj).GetComponent<MapBehaviour>();
                float swipeAmount = i * 0.5f;
                mapBehaviour.Initialise(_data.mapUiInfo[i], _gameData.GetSavedLevelData(_data.mapUiInfo[i].targetLvl), swipeAmount, this);
                mapBehaviours.Add(mapBehaviour);

            }
        }
        public void SetUpMaps()
        {
            mapBehaviours.ForEach(x => x.SetUpUI());
        }
        internal void Selected(SavedLevelData savedLevelData)
        {
            _mainMenuController.SetTargetLvl(savedLevelData.lvl, savedLevelData.IsOpen);
        }
    }


    [Serializable]
    public class MapPanelData
    {
        public GameObject mapUIPrefab;
        public Transform parentObj;
        public GameObject panel;

        public List<MapUiInfo> mapUiInfo;


    }

    [Serializable]
    public class MapUiInfo
    {
        public string name;
        public Sprite mapImg;
        public Sprite lockImg;
        public int targetLvl;
    }
}