using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.UI.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI
{

    public class UIMediator : MonoBehaviour
    {
        private LifetimeScope _parentScope;
        private LifetimeScope _uiScope;
        [SerializeField] private PlayerInGameUpgradeBarControllerData playerInGameUpgradeBarControllerData;
        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            CreateUIScope();

        }
        // private void Awake()
        // {
        //     DontDestroyOnLoad(this);
        // }
        private void CreateUIScope()
        {
            _uiScope = _parentScope.CreateChild(builder =>
           {
               builder.RegisterInstance(playerInGameUpgradeBarControllerData);
               builder.Register<PlayerInGameUpgradeBarController>(Lifetime.Scoped);
           });
        }
    }
}
