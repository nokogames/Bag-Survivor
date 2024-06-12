using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Interactable.Craft
{

    public class Gem : MonoBehaviour, ICraftable
    {
        private float _remainPercentage = 100;

        public bool CanCraftable => true;

        public Transform Transform => transform;

        public void Craft(float craftPercentage)
        {
            _remainPercentage -= craftPercentage;
            SetModelAndGemStatus();
        }

        private void SetModelAndGemStatus()
        {

        }
    }

}