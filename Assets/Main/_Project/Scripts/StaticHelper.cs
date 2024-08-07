using System;
using System.Collections;
using _Project.Scripts;
using _Project.Scripts.Interactable.Collectable;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StaticHelper : MonoBehaviour
{
    public static StaticHelper Instance;
    private GameStatus gameStatus;
    private void Awake()
    {
        Instance = this;
        gameStatus = GameStatus.Playing;
        DontDestroyOnLoad(this);

    }

    public void Start()
    {
        gameStatus = GameStatus.Playing;
    }
    public Coroutine StartHelperCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public IEnumerator MoveToPositionWithFollow(Transform target, Transform follower, CollectableType collectableType, Action<Transform, CollectableType> onCompleted)
    {
        float speed = 10f;
        while (target != null && target.position.CustomDistance(follower.position) > 1f)
        {
            var direction = follower.position - target.position;
            follower.position -= direction.normalized * speed * Time.deltaTime;
            speed -= .1f;
            speed = Mathf.Clamp(speed, 4, 10);
            yield return null;
        }
        onCompleted?.Invoke(follower, collectableType);
        yield return null;
    }

    public async UniTaskVoid FloatingTextAnim(Transform transform, TextMeshPro textMeshProUGUI, float duration = .5f)
    {
        // Duration of the floating text animation in seconds
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * 5; // The text floats upwards by 2 units
        Color startColor = textMeshProUGUI.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, .9f);
        // The text fades to transparent

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolate position
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Interpolate color
            textMeshProUGUI.color = Color.Lerp(startColor, endColor, t);

            await UniTask.Yield();
        }

        // Ensure the text reaches the final state
        transform.position = endPos;
        textMeshProUGUI.color = endColor;

        // Optionally, you can deactivate or destroy the text object after the animation
        textMeshProUGUI.gameObject.SetActive(false);
    }
    public async UniTaskVoid FloatingTextAnim(Transform transform, TextMeshPro textMeshProUGUI, float duration, Vector3 offsetEndPos)
    {
        // Duration of the floating text animation in seconds
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + offsetEndPos; // The text floats upwards by 2 units
        Color startColor = textMeshProUGUI.color;

        // The text fades to transparent

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolate position
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Interpolate color


            await UniTask.Yield();
        }

        // Ensure the text reaches the final state
        if (transform != null)
        {
            transform.position = endPos;
            // Optionally, you can deactivate or destroy the text object after the animation
            textMeshProUGUI.gameObject.SetActive(false);
        }


    }
}

