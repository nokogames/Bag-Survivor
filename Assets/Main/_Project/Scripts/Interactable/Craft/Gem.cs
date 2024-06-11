using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Interactable.Craft
{

    public class Gem : MonoBehaviour, ICraftable
    {
        public bool CanCraftable => true;

        public Transform Transform => transform;
    }

}