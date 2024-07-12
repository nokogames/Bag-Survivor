using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime.States;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Runtime
{

    public class AnimationEventHandler : MonoBehaviour
    {
        public Action OnCraft;
        public void Craft()
        {
            HapticManager.PlayHaptic(HapticType.SoftImpact);
            OnCraft?.Invoke();
        }
    }
}
