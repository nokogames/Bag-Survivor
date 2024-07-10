using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts;
using _Project.Scripts.Interactable.Craft;
using _Project.Scripts.Level;
using UnityEngine;
using VContainer;
namespace _Project.Scripts
{

    public class GemManager : MonoBehaviour
    {
        [Inject] private InLevelEvents inLevelEvents;
        private List<Gem> _gems;

        private void Awake()
        {
            _gems = GetComponentsInChildren<Gem>().ToList();
        }
        private void OnEnable()
        {
            inLevelEvents.onNextLevel += Reset;
            inLevelEvents.onNextSection += Reset;
        }
        private void OnDisable()
        {
            inLevelEvents.onNextLevel -= Reset;
            inLevelEvents.onNextSection -= Reset;
        }

        private void Reset()
        {
            _gems.ForEach(x => x.Reset());
        }
    }

}