using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.SkillManagement.Controllers;
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
        [SerializeField] private InGameSkillData inGameSkillData;

        // [Inject] private PlayerSM playerSM;

        [Inject] private UIMediator uiMediator;
        private LifetimeScope _skillManagerScope;
        private BarController _barController;
        public BarController BarController => _barController;

        public PlayerSM PlayerSM { get; internal set; }

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
                   builder.RegisterInstance(inGameSkillData);
                   builder.Register(_ => uiMediator.UIMediatorEventHandler, Lifetime.Scoped);
                   builder.Register(_ => uiMediator.SkillUIController, Lifetime.Scoped).AsSelf();
                   builder.Register(_ => uiMediator.PlayerInGameUpgradeBarController, Lifetime.Scoped);
                   builder.Register(_ => uiMediator.TutorialController, Lifetime.Scoped);
                   builder.RegisterEntryPoint<BarController>(Lifetime.Scoped).AsSelf();
                   builder.RegisterEntryPoint<SkillCreator>(Lifetime.Scoped).AsSelf();
                   builder.Register<SkillReciverController>(Lifetime.Scoped);
                   builder.Register<InGameSkillController>(Lifetime.Scoped);
                   // builder.RegisterInstance(Skills).AsSelf();

               });

            _skillManagerScope.name = "SkillManager Scope";
            _barController = _skillManagerScope.Container.Resolve<BarController>();




        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                _skillManagerScope.Container.Resolve<SkillCreator>().ReCreateSkill();
                _skillManagerScope.Container.Resolve<SkillUIController>().ShowPanel();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {

                _barController.Upgraded();
                _skillManagerScope.Container.Resolve<SkillUIController>().HidePanel();

            }
        }

        private void Start()
        {

            // _skillManagerScope.Container.Resolve<SkillCreator>().Start();
            _skillManagerScope.Container.Resolve<SkillReciverController>().Start();
            _skillManagerScope.Container.Resolve<InGameSkillController>().Start(PlayerSM.Transform);
            _barController.Start();

        }
        private void OnDestroy()
        {

            _skillManagerScope.Dispose();
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
