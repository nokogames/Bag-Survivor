using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Level;
using _Project.Scripts.UI.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI
{

    public class UIMediator : MonoBehaviour
    {
        private LifetimeScope _parentScope;
        public LifetimeScope _uiScope;
        [Header("In game elements"), SerializeField] private InGamePanelView inGamePanelView;
        [Header("Level End Collected PlayTime"), SerializeField] private LevelEndDataPanelView levelEndDataPanelView;
        [Header("Main Menu Data"), SerializeField] private MainMenuControllerData mainMenuControllerData;
        [Header("Panels"), SerializeField] private PanelControllerData panelControllerData;
        [Header("Bar"), SerializeField] private PlayerInGameUpgradeBarControllerData playerInGameUpgradeBarControllerData;
        [SerializeField] private SkillUIControllerData sillUIControllerData;
        [SerializeField] private SectionUIControllerData sectionUIControllerData;

        public PlayerInGameUpgradeBarController PlayerInGameUpgradeBarController { get; private set; }
        public SkillUIController SkillUIController { get; set; }
        public UIMediatorEventHandler UIMediatorEventHandler { get; set; }
        public PanelController PanelController { get; private set; }
        public InGamePanelController InGamePanelController { get; private set; }
        public LevelEndDataPanel LevelEndDataPanel { get; private set; }

        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            Debug.Log("ParentScope", parentScope.transform);
            CreateUIScope();

        }

        private void CreateUIScope()
        {
            _uiScope = _parentScope.CreateChild(builder =>
           {
               builder.RegisterComponent(GetComponent<UIMediatorEventHandler>());
               builder.RegisterInstance(playerInGameUpgradeBarControllerData);
               builder.RegisterInstance(sillUIControllerData);
               builder.RegisterInstance(sectionUIControllerData);
               builder.RegisterInstance(panelControllerData);
               builder.RegisterInstance(mainMenuControllerData);
               builder.RegisterInstance(levelEndDataPanelView);
               builder.RegisterInstance(inGamePanelView);

               builder.RegisterEntryPoint<PlayerInGameUpgradeBarController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<SkillUIController>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
               builder.RegisterEntryPoint<PanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<MainMenuController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<LevelEndDataPanel>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<InGamePanelController>(Lifetime.Scoped).AsSelf();



               builder.Register<SectionUIController>(Lifetime.Scoped);

           });
            _uiScope.name = "UIMediatorScope";
            Resolve();

        }
        private void Start()
        {

        }
        private void Resolve()
        {
            PlayerInGameUpgradeBarController = _uiScope.Container.Resolve<PlayerInGameUpgradeBarController>();
            SkillUIController = _uiScope.Container.Resolve<SkillUIController>();
            UIMediatorEventHandler = _uiScope.Container.Resolve<UIMediatorEventHandler>();
            PanelController = _uiScope.Container.Resolve<PanelController>();
            InGamePanelController = _uiScope.Container.Resolve<InGamePanelController>();
            LevelEndDataPanel = _uiScope.Container.Resolve<LevelEndDataPanel>();
        }
    }



}
