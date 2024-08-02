using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkillBehaviours : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Image bgImg;

    public void Initialize(Sprite sprite)
    {
        bgImg.sprite = sprite;
    }
    public void SetBar(float value)
    {
        bar.fillAmount = value;
    }
}
