using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Interactable.Craft;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Craft
{

    public class CraftDetector : MonoBehaviour
    {
        private ICraftDetectorReciver _reciver;
        private ICraftable _crrCraftable;
        public void Initialise(ICraftDetectorReciver reciver)
        {
            _reciver = reciver;
            
        }
        private void OnTriggerEnter(Collider other)
        {

            if (!other.transform.CompareTag(CRAFTABLE_TAG)) return;

            if (other.transform.TryGetComponent<ICraftable>(out ICraftable craftable))
            {
                _crrCraftable = craftable;
                _reciver.OnCraftableDetect(_crrCraftable);
            }
        }

        private float _crrTime = 0;
        public void FixedUpdate()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < .1f) return;
            _crrTime = 0;
            if (_crrCraftable != null && !_crrCraftable.CanCraftable)
            {
                _crrCraftable = null;
                _reciver.OnCraftableDetect(null);
            }

        }
        private void OnTriggerExit(Collider other)
        {

            if (!other.transform.CompareTag(CRAFTABLE_TAG)) return;

            if (other.transform.TryGetComponent<ICraftable>(out ICraftable craftable))
            {
                _crrCraftable = null;
                _reciver.OnCraftableDetect(null);
            }
        }
        private readonly string CRAFTABLE_TAG = "Craftable";
        public void ClearDeatected()
        {
            _crrCraftable = null;
        }
    }
}
