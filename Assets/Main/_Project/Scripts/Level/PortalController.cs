using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Level
{

    public class PortalController : MonoBehaviour
    {
        [Inject] private PlayerSM _playerSM;
        [Inject] private InLevelEvents _inLevelEvents;
        [SerializeField] private GameObject model;
        private Collider _collider;

        private bool _isTransitioning;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
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
            transform.position = _playerSM.Transform.position + Vector3.forward * 3f;

            model.SetActive(true);
            _collider.enabled = true;

        }

        public void HidePortal()
        {

            model.SetActive(false);
            _collider.enabled = false;
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