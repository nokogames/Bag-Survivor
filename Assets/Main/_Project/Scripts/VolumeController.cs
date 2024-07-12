using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using MoreMountains.FeedbacksForThirdParty;
using Lofelt.NiceVibrations;
namespace _Project.Scripts
{


    public class VolumeController : MonoBehaviour
    {
        private MMChromaticAberrationShaker_URP chormatic;
        private void Awake()
        {
            chormatic = GetComponent<MMChromaticAberrationShaker_URP>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                chormatic.StartShaking();
            }
        }

        public void PlayChromatic()
        {
            chormatic.StartShaking();
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }
        // private Volume _volume;

        // private void Awake()
        // {
        //     var _volume = gameObject.GetComponent<Volume>();
        // }
    }

}