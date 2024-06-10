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

}

