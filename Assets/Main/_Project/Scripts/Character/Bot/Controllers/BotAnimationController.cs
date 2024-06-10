using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Bot
{

    public class BotAnimationController
    {
        [Inject] private Animator _animator;
        [Inject] private IBot _bot;
        [Inject] private BotAgentController _botAgentController;

        public void SetActiveAnimation(bool isActive)
        {
            _animator.SetBool(ACTIVE_ANIM_NAME, isActive);
        }
        private Tween _placeTween;
        private Tween _placeScaleTween;
        private Tween _placeRotationTween;
        public void Place()
        {
            if (_placeTween != null) _placeTween.Kill();
            if (_placeScaleTween != null) _placeScaleTween.Kill();
            if (_placeRotationTween != null) _placeRotationTween.Kill();

            SetActiveAnimation(false);
            _botAgentController.SetAgentStatus(false);
            _placeScaleTween = _bot.Transform.DOScale(.3f, .5f).SetDelay(.1f);
            _placeRotationTween = _bot.Transform.DOLocalRotate(Vector3.zero, .2f);
            StaticHelper.Instance.StartCoroutine(_bot.Transform.CustomDoJump(_bot.PlayerPlacePoint, 2f, .5f, Placed));
        }
        private void Placed()
        {
            _bot.Transform.parent = _bot.PlayerPlacePoint;
        }
        //  public void Place()
        // {
        //     if (_placeTween != null) _placeTween.Kill();
        //     if (_placeScaleTween != null) _placeScaleTween.Kill();

        //     SetActiveAnimation(false);
        //     _botAgentController.SetAgentStatus(false);
        //     _bot.Transform.parent = _bot.PlayerPlacePoint;
        //     _bot.Transform.DOLocalJump(Vector3.zero, -1f, 1, 1f).SetDelay(.1f);
        //     _bot.Transform.DOScale(.3f, .5f).SetDelay(.1f); ;
        // }
        public void UnPlace()
        {
            if (_placeTween != null) _placeTween.Kill();
            if (_placeScaleTween != null) _placeScaleTween.Kill();
            if (_placeRotationTween != null) _placeRotationTween.Kill();

            _bot.Transform.parent = null;

            _placeTween = _bot.Transform.DOJump(Vector3.zero + new Vector3(Random.Range(0, 2f), 0, Random.Range(0, 2f)), .5f, 2, 1f).OnComplete(() =>
                {
                    _botAgentController.SetAgentStatus(true);
                });

            _placeRotationTween = _bot.Transform.DOLocalRotate(Vector3.zero, .2f);
            _placeScaleTween = _bot.Transform.DOScale(1f, .5f).OnComplete(() => SetActiveAnimation(true));
        }

        private static readonly string ACTIVE_ANIM_NAME = "active";

    }

}
