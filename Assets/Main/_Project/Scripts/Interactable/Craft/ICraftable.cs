using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Craft;
using UnityEngine;

namespace _Project.Scripts.Interactable.Craft
{

    public interface ICraftable : ITargetable
    {
        public bool CanCraftable { get; }
        public void Craft(float craftPercentage);
    }
}
