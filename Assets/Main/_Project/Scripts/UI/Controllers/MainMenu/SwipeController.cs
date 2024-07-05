
using System;
using System.Collections.Generic;
using _Project.Scripts.Level;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers.MainMenu
{
    public class SwipeController : IStartable, ITickable
    {

        [Inject] private MainMenuView _data;
        private float _scrollAmountPerPage;
        private float _mapScrollAmountPerPage;


        public void Start()
        {
            _data.mainScrollBar.value = 0.5f;
            _scrollAmountPerPage = 1f / (_data.pages.Count - 1);

            _data.mapScrollBar.value = 0;
            _mapScrollAmountPerPage = 1f / (_data.mapBehaviours.Count - 1);

            _data.menuBtnBehaviours.ForEach(x => x.Initialize(MoveMainPanel));
            _data.leftBtn.onClick.AddListener(() => MapMoveToLeft());
            _data.rightBtn.onClick.AddListener(() => MapMoveToRight());


        }

        private Vector3 _mouseStartPos;

        public void Tick()
        {
            _data.menuBtnBehaviours.ForEach(x => x.SetAnimByValue(_data.mainScrollBar.value));
            _data.mapBehaviours.ForEach(x => x.SetAnimByValue(_data.mapScrollBar.value));

            if (Input.GetMouseButtonUp(0))
            {
                MainClampAnim();
                MapClampAnim();
            }

        }
        public void MoveMainPanel(float targetValue)
        {
            if (_mainAnim != null) _mainAnim.Kill();
            // _data.mainScrollBar.value = targetValue;
            _mainAnim = DOTween.To(() => _data.mainScrollBar.value, x => _data.mainScrollBar.value = x, targetValue, 0.2f).SetEase(Ease.Linear);

        }
        public void MapMoveToLeft()
        {
            if (_mapAnim != null) _mapAnim.Kill();
            var crrValue = _data.mapScrollBar.value;
            _mapAnim = DOTween.To(() => _data.mapScrollBar.value, x => _data.mapScrollBar.value = x, Mathf.Clamp(crrValue - _mapScrollAmountPerPage, 0, 1), 0.2f).SetEase(Ease.Linear);
        }
        public void MapMoveToRight()
        {
            if (_mapAnim != null) _mapAnim.Kill();
            var crrValue = _data.mapScrollBar.value;
            _mapAnim = DOTween.To(() => _data.mapScrollBar.value, x => _data.mapScrollBar.value = x, Mathf.Clamp(crrValue + _mapScrollAmountPerPage, 0, 1), 0.2f).SetEase(Ease.Linear);
        }
        private Tween _mainAnim;
        private Tween _mapAnim;
        private void MapClampAnim()
        {
            float remain = _data.mapScrollBar.value % _mapScrollAmountPerPage;
            if (remain == 0) return;
            if (_mainAnim != null) _mainAnim.Kill();
            float result = remain > (_mapScrollAmountPerPage / 2f) ? (_mapScrollAmountPerPage - remain) + _data.mapScrollBar.value : _data.mapScrollBar.value - remain;
            var dest = Mathf.Clamp(result, 0f, 1f);
            _mainAnim = DOTween.To(() => _data.mapScrollBar.value, x => _data.mapScrollBar.value = x, dest, 0.05f).SetEase(Ease.Linear);
        }

        private void MainClampAnim()
        {
            float remain = _data.mainScrollBar.value % _scrollAmountPerPage;
            if (remain == 0) return;
            if (_mapAnim != null) _mapAnim.Kill();
            float result = remain > (_scrollAmountPerPage / 2f) ? (_scrollAmountPerPage - remain) + _data.mainScrollBar.value : _data.mainScrollBar.value - remain;
            var dest = Mathf.Clamp(result, 0f, 1f);
            _mapAnim = DOTween.To(() => _data.mainScrollBar.value, x => _data.mainScrollBar.value = x, dest, 0.05f).SetEase(Ease.Linear);
        }
    }
}