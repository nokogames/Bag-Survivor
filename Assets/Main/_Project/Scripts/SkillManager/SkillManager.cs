using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.SkillManager.Controllers;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.UI.Interfacies;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.SkillManager
{

    public class SkillManager : MonoBehaviour
    {
        [Inject] private PlayerSM playerSM;
        [Inject] private UIMediator uiMediator;
        private LifetimeScope _uiScope;

        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            _uiScope = uiMediator._uiScope;
            CreateScope(parentScope);
        }

        private void CreateScope(LifetimeScope parentScope)
        {
            var skillManagerScope = parentScope.CreateChild(builder =>
                {
                    builder.Register(_ => uiMediator.UIMediatorEventHandler, Lifetime.Scoped);
                    builder.RegisterEntryPoint<SkillReciverController>(Lifetime.Scoped);

                });
            Resolve();

        }


        private void Resolve()
        {

        }

        private void Start()
        {

        }


    }


    public enum SkillRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}
