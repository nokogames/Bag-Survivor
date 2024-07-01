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
            model.SetActive(true);
            _collider.enabled = true;
            transform.position = Vector3.zero;
        }

        public void HidePortal()
        {

            model.SetActive(false);
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTransitioning) return;
            if (other.gameObject.TryGetComponent<PlayerSM>(out PlayerSM player)) NextSection();

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


        private void SubscribeEvents()
        {
            _inLevelEvents.onAbleToNextSection += ShowPortal;
            _inLevelEvents.onNextSection += SectionStarted;

        }

        private void UnsubscribeEvents()
        {
            _inLevelEvents.onAbleToNextSection -= ShowPortal;
            _inLevelEvents.onNextSection -= SectionStarted;
        }
    }

}