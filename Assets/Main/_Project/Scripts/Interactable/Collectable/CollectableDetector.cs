using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Interactable.Collectable
{
    public class CollectableDetector : MonoBehaviour
    {
        private ICollectableDetectorReciver _reciver;
        public void Initialise(ICollectableDetectorReciver reciver)
        {
            _reciver = reciver;
        }
        public void SetRadius(float radius)
        {
            transform.localScale = new Vector3(radius, radius, radius);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.TryGetEnumType<CollectableType>(out CollectableType type))
            {
              
                _reciver.OnCollectableDetected(type, other.transform);
            };
            //Check if tag xp or gem  it  will be true
        }
    }


    [Flags]
    public enum CollectableType
    {
        XP = 0,
        GEM = 1
    }
}
