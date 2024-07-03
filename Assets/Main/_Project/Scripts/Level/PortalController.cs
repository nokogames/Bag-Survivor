using System;
using System.Collections;
using System.Collections.Generic;

using _Project.Scripts.Character.Runtime;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Level
{

    public class PortalController : MonoBehaviour
    {
        [Inject] private PlayerSM _playerSM;
        [Inject] private InLevelEvents _inLevelEvents;
        [SerializeField] private MeshRenderer arrowMeshRenderer;
        private Material _tutorialArrowMat;
        [SerializeField] private GameObject arrowObj;
        [SerializeField] private GameObject model;
        private Collider _collider;

        private bool _isTransitioning;
        private Tween _arrowTween;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _tutorialArrowMat = arrowMeshRenderer.material;
        }
        private void OnEnable()
        {
            SubscribeEvents();

            _isTransitioning = false;
            HidePortal();

        }


        private void OnDisable()
        {
            UnsubscribeEvents();


        }

        private void ShowPortal()
        {
            transform.position = _playerSM.Transform.position + Vector3.forward * 4f;

            model.SetActive(true);
            _collider.enabled = true;
            StartArrow();

        }

        public void HidePortal()
        {

            model.SetActive(false);
            _collider.enabled = false;
            StopArrow();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTransitioning) return;
            if (other.gameObject.TryGetComponent<PlayerSM>(out PlayerSM player))
            {

                if (_isLevelChanging) NextLevel();
                else NextSection();
            }

        }
        public void StartArrow()
        {
            arrowObj.SetActive(true);
            var targetOffset = new Vector2(-5, 0);
            _arrowTween = _tutorialArrowMat.DOOffset(targetOffset, 5f)
                   .SetEase(Ease.Linear) // Set easing type
                   .SetLoops(-1, LoopType.Incremental) // Loop indefinitely with incremental change
                   .OnUpdate(() =>
                   {
                       CalculateScale();
                       // Update the material's offset in each frame
                       // material.mainTextureOffset = material.mainTextureOffset;
                   })
                   .OnKill(() =>
                   {
                       // Reset the offset to the initial value when the tween is killed
                       _tutorialArrowMat.mainTextureOffset = new Vector2(0, 0);
                   });
        }
        public void StopArrow()
        {
            if (_arrowTween != null) _arrowTween.Kill();
            arrowObj.SetActive(false);
        }
        private void CalculateScale()
        {
            arrowObj.transform.LookAt(_playerSM.Transform);
            var distance = Vector2.Distance(new Vector2(_playerSM.Transform.position.x, _playerSM.Transform.position.z), new Vector2(transform.position.x, transform.position.z));
            var localScale = arrowObj.transform.localScale;
            arrowObj.transform.localScale = new Vector3(localScale.x, localScale.y, distance);
            _tutorialArrowMat.mainTextureScale = new Vector2(distance, 1);
        }

        private void NextLevel()
        {
            _isTransitioning = true;
            _inLevelEvents.onShowNextLevelUI?.Invoke();
        }

        private void NextSection()
        {

            _isTransitioning = true;
            _inLevelEvents.onShowNextSectionUI?.Invoke();
            //Show collected  resource ui;
        }


        public void SectionStarted()
        {
            _isTransitioning = false;
            HidePortal();

        }

        private bool _isLevelChanging;
        private void AbleToNextLevel()
        {
            _isLevelChanging = true;
            ShowPortal();

        }
        private void SubscribeEvents()
        {
            _inLevelEvents.onAbleToNextSection += ShowPortal;
            _inLevelEvents.onNextSection += SectionStarted;
            _inLevelEvents.onAbleToNextLevel += AbleToNextLevel;

        }



        private void UnsubscribeEvents()
        {
            _inLevelEvents.onAbleToNextLevel -= AbleToNextLevel;
            _inLevelEvents.onAbleToNextSection -= ShowPortal;
            _inLevelEvents.onNextSection -= SectionStarted;
        }
    }

}