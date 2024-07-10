using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.UI.Controllers.MainMenu;
using Pack.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private Image lockImg;
    [SerializeField] private TextMeshProUGUI playedTimeTxt;
    [SerializeField] private TextMeshProUGUI percentTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;
    public float TargetSwipeValue = 0;
    public float PerPageSwipeValue = 0.5f;
    public int TargetLvl { get; set; }
    private Image _bgImg;
    private MapUiInfo _mapUiInfo;
    SavedLevelData _savedLevelData;
    private MapPanelController _mapPanelController;
    private void Awake()
    {
        _bgImg = GetComponent<Image>();
    }
    public void SetAnimByValue(float scrollValue)
    {


        float distance = Mathf.Abs(scrollValue - TargetSwipeValue);


        float normalizedDistance = Mathf.Clamp01(distance / PerPageSwipeValue);


        float result = Mathf.Lerp(1, 0, normalizedDistance);
        result = Mathf.Lerp(.5f, 1f, result);

        transform.localScale = new Vector3(result, result, result);




    }


    public void Initialise(MapUiInfo uiInfo, SavedLevelData savedLevelData, float targetSwipe, MapPanelController mapPanelController)
    {
        _mapUiInfo = uiInfo;
        _savedLevelData = savedLevelData;
        TargetSwipeValue = targetSwipe;
        _mapPanelController = mapPanelController;
        SetupView();
        //lockImg
    }

    private void SetupView()
    {
        _bgImg.sprite = _mapUiInfo.mapImg;
        lockImg.sprite = _mapUiInfo.lockImg;
        lockImg.enabled = !_savedLevelData.IsOpen;
        nameTxt.text=_mapUiInfo.name;
        if (_savedLevelData.MaxClearedEnemyPercentage > 1)
        {
            percentTxt.text = $"Level <color=red> {_savedLevelData.MaxClearedEnemyPercentage}%</color> Cleared";
        }
            

    }

    internal void Selected(float dest)
    {
        if (dest != TargetSwipeValue) return;
        _mapPanelController.Selected(_savedLevelData);
    }

    internal void SetUpUI()
    {
        SetupView();
    }
}
