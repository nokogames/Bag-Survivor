using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtnBehaviour : MonoBehaviour
{
    public float TargetSwipeValue = 0;
    public float PerPageSwipeValue = 0.5f;
    private Animator _animator;
    private Button _button;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    public void SetAnimByValue(float scrollValue)
    {

        // Butonun hedefine olan uzaklığını hesaplayın
        float distance = Mathf.Abs(scrollValue - TargetSwipeValue);

        // Uzaklığı normalize edin ve 0 ile 1 arasında bir değere çevirin
        float normalizedDistance = Mathf.Clamp01(distance / PerPageSwipeValue);

        // NormalizedDistance değeri ile animasyon blend değerini hesaplayın
        float result = Mathf.Lerp(1f, 0f, normalizedDistance);

        // Animatördeki Blend parametresini ayarlayın
        _animator.SetFloat("Blend", result);

        if (result > .9) SetChildIndex(3);
        else SetChildIndex(0);


    }

    private void SetChildIndex(int index)
    {
        transform.SetSiblingIndex(index);
    }

    internal void Initialize(Action<float> moveMainPanel)
    {
        _button.onClick.AddListener(() => moveMainPanel(TargetSwipeValue));
    }
}
