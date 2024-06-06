using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Reusable{

public class LookToCamDirection : MonoBehaviour
{
    private Transform _mainCamTransform;
    private void Start()
    {
        _mainCamTransform = Camera.main.transform;
    }
    private void Update()
    {
        // transform.forward = Camera.main.transform.right;
        transform.LookAt(_mainCamTransform);
    }
}

}