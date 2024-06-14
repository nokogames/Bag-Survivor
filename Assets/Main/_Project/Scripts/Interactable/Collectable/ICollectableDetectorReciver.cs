using UnityEngine;

namespace _Project.Scripts.Interactable.Collectable
{
    public interface ICollectableDetectorReciver
    {
        public void OnCollectableDetected(CollectableType collectableType, Transform transform);
    }
}