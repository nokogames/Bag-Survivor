using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public float TargetSwipeValue = 0;
    public float PerPageSwipeValue = 0.5f;



    public void SetAnimByValue(float scrollValue)
    {

        // Butonun hedefine olan uzaklığını hesaplayın
        float distance = Mathf.Abs(scrollValue - TargetSwipeValue);

        // Uzaklığı normalize edin ve 0 ile 1 arasında bir değere çevirin
        float normalizedDistance = Mathf.Clamp01(distance / PerPageSwipeValue);

        // NormalizedDistance değeri ile animasyon blend değerini hesaplayın
        float result = Mathf.Lerp(1, 0, normalizedDistance);
        result = Mathf.Lerp(.5f, 1f, result);
        // Animatördeki Blend parametresini ayarlayın
        transform.localScale = new Vector3(result, result, result);




    }
}
