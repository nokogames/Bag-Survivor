using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Interactable.Craft
{

    public class Gem : MonoBehaviour, ICraftable
    {
        private float _remainPercentage = 100;

        private bool _canCraftable = true;
        public bool CanCraftable => _canCraftable;

        public Transform Transform => transform;
        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        public void Craft(float craftPercentage)
        {
            _remainPercentage -= craftPercentage;
            SetModelAndGemStatus();
        }

        private void SetModelAndGemStatus()
        {
            if (_remainPercentage <= 0)
            {
                Crafted();
            }
        }

        private void Crafted()
        {
            _canCraftable = false;
            _collider.enabled = false;
        }
    }

}