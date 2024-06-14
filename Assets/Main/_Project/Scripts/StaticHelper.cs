using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHelper : MonoBehaviour
{
    public static StaticHelper Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }


    public Coroutine StartHelperCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public IEnumerator MoveToPositionWithFollow(Transform target, Transform follower, Action<Transform> onCompleted)
    {
        float speed = 10f;
        while (target.position.CustomDistance(follower.position) > 1f)
        {
            var direction = follower.position - target.position;
            follower.position -= direction.normalized * speed * Time.deltaTime;
            speed -= .1f;
            speed = Mathf.Clamp(speed, 4, 10);
            yield return null;
        }
        onCompleted?.Invoke(follower);
        yield return null;
    }

}

