using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Interactable.Craft;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Craft
{

    public class CraftDetector : MonoBehaviour
    {
        [Inject] private ICraftDetectorReciver _reciver;
        private ICraftable _crrCraftable;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Reciver -{_reciver}");
            if (!other.transform.CompareTag(CRAFTABLE_TAG)) return;

            if (other.transform.TryGetComponent<ICraftable>(out ICraftable craftable))
            {
                _crrCraftable = craftable;
                _reciver.OnCraftableDetect(_crrCraftable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"Reciver -{_reciver}");
            if (!other.transform.CompareTag(CRAFTABLE_TAG)) return;
            
            if (other.transform.TryGetComponent<ICraftable>(out ICraftable craftable))
            {
                _crrCraftable = null;
                _reciver.OnCraftableDetect(null);
            }
        }
        private readonly string CRAFTABLE_TAG = "Craftable";

    }
}
