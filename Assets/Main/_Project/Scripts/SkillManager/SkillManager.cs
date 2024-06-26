using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.UI.Interfacies;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.SkillManagement
{

    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private List<SkillBase> Skills;
        [Inject] private PlayerSM playerSM;
        [Inject] private UIMediator uiMediator;
        private LifetimeScope _skillManagerScope;
        public BarController BarController { get => _skillManagerScope.Container.Resolve<BarController>(); }

        // public BarController BarController { get => null; }

        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            CreateScope(parentScope);
        }

        private void CreateScope(LifetimeScope parentScope)
        {
            _skillManagerScope = parentScope.CreateChild(builder =>
               {
                   builder.Register(_ => uiMediator.UIMediatorEventHandler, Lifetime.Scoped);
                   builder.Register(_ => uiMediator.SkillUIController, Lifetime.Scoped);
                   builder.Register(_ => uiMediator.PlayerInGameUpgradeBarController, Lifetime.Scoped);

                     builder.RegisterEntryPoint<BarController>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
                      builder.RegisterEntryPoint<SkillCreator>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
                    //   builder.RegisterEntryPoint<SkillReciverController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
                      builder.RegisterInstance(Skills).AsSelf();

               });

            _skillManagerScope.name = "SkillManager Scope";


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
