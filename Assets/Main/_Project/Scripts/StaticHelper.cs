using System;
using System.Collections;
using _Project.Scripts.Interactable.Collectable;
using UnityEngine;

public class StaticHelper : MonoBehaviour
{
    public static StaticHelper Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

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


}

