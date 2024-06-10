using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtentions
{
    public static IEnumerator CustomDoJump(this Transform transform, Transform targetPosition, float jumpHeight, float duration, Action OnCompleted = null)
    {
        Vector3 startPosition = transform.position;
        Vector3 peakPosition = new Vector3((startPosition.x + targetPosition.position.x) / 2, startPosition.y + jumpHeight, (startPosition.z + targetPosition.position.z) / 2);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            // Animasyonun tamamlanma oranını hesapla
            float t = elapsedTime / duration;

            // Yükseklik için parabolik bir interpolasyon yap
            float heightAtT = Mathf.Sin(Mathf.PI * t) * jumpHeight;

            // Yatay pozisyonu lineer interpolasyonla hesapla
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, t) + new Vector3(0, heightAtT, 0);

            // Zamanı güncelle
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Emin olmak için son pozisyonu ayarla
        transform.position = targetPosition.position;
        OnCompleted?.Invoke();
    }
}
