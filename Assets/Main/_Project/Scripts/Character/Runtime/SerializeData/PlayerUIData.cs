using System;
using UnityEngine;
using UnityEngine.UI;
namespace _Project.Scripts.Character.Runtime.SerializeData
{

    [Serializable]
    public class PlayerUIData
    {
        public Image barImg;
        public Transform barTransform;


        public float BarFillAomunt { get => barImg.fillAmount; set => barImg.fillAmount = value; }
        public bool EnabledBar { get => barTransform.gameObject.activeSelf; set => barTransform.gameObject.SetActive(value); }
    }
}