using System;
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
        // [Inject] private BotAgentController _botAgentController;

        private void SetActiveAnimation(bool isActive)
        {
            _animator.SetBool(ACTIVE_ANIM_NAME, isActive);
        }

        public void Move()
        {
            _animator.SetFloat(HORIZONTAL_ANIM_NAME, 1f);
        }
        public void Stop()
        {
            _animator.SetFloat(HORIZONTAL_ANIM_NAME, 0f);
        }

        #region  custom


        private Tween _placeTween;
        private Tween _placeScaleTween;
        private Tween _placeRotationTween;
        private Coroutine _openCorotine;
        public void Place(Action onCompleted = null)
        {
            KillAllCustomAnims();
            SetActiveAnimation(false);
            // _botAgentController.SetAgentStatus(false);
            _placeScaleTween = _bot.Transform.DOScale(.3f, .3f).SetDelay(.1f);
            _placeRotationTween = _bot.Transform.DOLocalRotate(Vector3.zero, .2f);
            _openCorotine = StaticHelper.Instance.StartCoroutine(_bot.Transform.CustomDoJump(_bot.PlayerPlacePoint, 2f, .5f, () => Placed(onCompleted)));
        }
        private void Placed(Action onCompleted)
        {
            _bot.Transform.parent = _bot.PlayerPlacePoint;
            onCompleted?.Invoke();

        }

        public void UnPlace(Action onCompleted = null)
        {
            KillAllCustomAnims();
            _bot.Transform.parent = null;

            Vector3 destinationPos = _bot.UnPlacePoint.position;
            destinationPos.y = 0;
            _placeTween = _bot.Transform.DOJump(destinationPos, .5f, 2, .5f)
            .OnComplete(() =>
            {
                // _botAgentController.SetAgentStatus(true);
                onCompleted?.Invoke();

            });

            _placeRotationTween = _bot.Transform.DOLocalRotate(Vector3.zero, .2f);
            _placeScaleTween = _bot.Transform.DOScale(1f, .3f).OnComplete(() => SetActiveAnimation(true));
        }

        private void KillAllCustomAnims()
        {
            if (_placeTween != null) _placeTween.Kill();
            if (_placeScaleTween != null) _placeScaleTween.Kill();
            if (_placeRotationTween != null) _placeRotationTween.Kill();
            if (_openCorotine != null) StaticHelper.Instance.StopCoroutine(_openCorotine);
        }

        private static readonly string ACTIVE_ANIM_NAME = "active";
        #endregion
        private static readonly string HORIZONTAL_ANIM_NAME = "horizontal";
        private static readonly string VERTICLEL_ANIM_NAME = "verticle";

    }

}
