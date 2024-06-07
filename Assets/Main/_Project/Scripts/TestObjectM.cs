using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestObjectM : MonoBehaviour
{
    private LifetimeScope _parentScope;
    private LifetimeScope _playerScope;
    [Inject]
    public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
    {
        _parentScope = parentScope;
        CreatePlayerScope();
    }
    private void CreatePlayerScope()
    {
        _playerScope = _parentScope.CreateChild(builder =>
        {





        });
    }

    public bool IsWork() => true;
}
