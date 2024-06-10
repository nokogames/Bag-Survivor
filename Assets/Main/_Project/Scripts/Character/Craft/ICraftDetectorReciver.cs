using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Interactable.Craft;
using UnityEngine;

namespace _Project.Scripts.Character.Craft
{

    public interface ICraftDetectorReciver
    {
        void OnCraftableDetect(ICraftable crrCraftable);
    }
}
