using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Level;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.UI.Controllers.MainMenu;
using _Project.Scripts.UI.Inventory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI
{

    public class UIMediator : MonoBehaviour
    {

        [SerializeField] private TutorialController tutorialController;
        [Header("Active Skill Ui Data"), SerializeField] private ActiveSkillUIControllerData activeSkillUIControllerData;
        [Header("Custom Inventory"), SerializeField]
        CustomInventoryData customInventoryData;

        [Header("Inventory"), SerializeField]
        private InventoryTetrisAssets inventoryTetrisAssets;
        [SerializeField] private InventoryTetrisDragDropSystem inventoryTetrisDragDropSystem;
        [SerializeField] private CameraTarget cameraTarget;
        [SerializeField] private GameAssets gameAssets;
        [SerializeField] private InventoryTetrisTesting inventoryTetrisTesting;
        [SerializeField] private InventoryData inventoryData;

        private LifetimeScope _parentScope;
        public LifetimeScope _uiScope;
        [SerializeField] private UpgradeVisualController upgradeVisualControllerPref;
        [Header("Map Panel Data"), SerializeField] private MapPanelData mapPanelData;
        [Header("Player Upgrade Panel Data"), SerializeField] private PlayerUpgradePanelData playerUpgradePanelData;
        [Header("Bot Upgrade Panel Data"), SerializeField] private BotUpgradePanelData botUpgradePanelData;
        [Header("In game elements"), SerializeField] private InGamePanelView inGamePanelView;
        [Header("Level End Collected PlayTime"), SerializeField] private LevelEndDataPanelView levelEndDataPanelView;
        [Header("Main Menu Data"), SerializeField] private MainMenuView mainMenuControllerData;
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
        public TutorialController TutorialController => tutorialController;
        private InventoryManager _inventoryManager;
        private Transform _playerTransform;
        public Transform PlayerTransform
        {
            get { return _playerTransform; }
            set
            {
                _playerTransform = value;
                if (_inventoryManager != null) _inventoryManager.PlayerTransform = value;
            }
        }

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
               //    builder.RegisterComponentInNewPrefab(inventoryTetrisAssets, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
               //    builder.RegisterComponentInNewPrefab(inventoryTetrisDragDropSystem, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
               //    builder.RegisterComponentInNewPrefab(cameraTarget, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
               //    builder.RegisterComponentInNewPrefab(gameAssets, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
               //    builder.RegisterComponentInNewPrefab(inventoryTetrisTesting, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();

               //    builder.RegisterInstance(inventoryData.Assets);
               //    builder.RegisterInstance(inventoryData.InventoryDragDropData);
               //    builder.RegisterInstance(inventoryData.testingData);

               builder.RegisterComponent(GetComponent<UIMediatorEventHandler>());

               builder.RegisterInstance(playerInGameUpgradeBarControllerData);
               builder.RegisterInstance(sillUIControllerData);
               builder.RegisterInstance(sectionUIControllerData);
               builder.RegisterInstance(panelControllerData);
               builder.RegisterInstance(mainMenuControllerData);
               builder.RegisterInstance(levelEndDataPanelView);
               builder.RegisterInstance(inGamePanelView);
               builder.RegisterInstance(playerUpgradePanelData);
               builder.RegisterInstance(botUpgradePanelData);
               builder.RegisterInstance(mapPanelData);
               builder.RegisterInstance(customInventoryData);
               builder.RegisterInstance(activeSkillUIControllerData);

               builder.RegisterComponent(tutorialController);

               builder.RegisterComponentInNewPrefab(upgradeVisualControllerPref, Lifetime.Scoped).DontDestroyOnLoad();

               builder.RegisterEntryPoint<PlayerInGameUpgradeBarController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<SkillUIController>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
               builder.RegisterEntryPoint<PanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<MainMenuController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<LevelEndDataPanel>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<InGamePanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<SwipeController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<MainMenuPlayerUpgradePanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<MainMenuBotUpgradePanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<MapPanelController>(Lifetime.Scoped).AsSelf();
               builder.RegisterEntryPoint<ActiveSkillUIController>(Lifetime.Scoped).AsSelf();

               builder.RegisterEntryPoint<InventoryManager>(Lifetime.Scoped).AsSelf();
               builder.Register<InventoryGridFactory>(Lifetime.Scoped).AsSelf();



               builder.Register<SectionUIController>(Lifetime.Scoped);

           });
            _uiScope.name = "UIMediatorScope";
            Resolve();

        }
        private void Start()
        {
            // _uiScope.Container.Resolve<InventoryTetrisAssets>();
            // _uiScope.Container.Resolve<InventoryTetrisDragDropSystem>();
            // _uiScope.Container.Resolve<InventoryTetrisTesting>();
        }
        private void Resolve()
        {
            PlayerInGameUpgradeBarController = _uiScope.Container.Resolve<PlayerInGameUpgradeBarController>();
            SkillUIController = _uiScope.Container.Resolve<SkillUIController>();
            UIMediatorEventHandler = _uiScope.Container.Resolve<UIMediatorEventHandler>();
            PanelController = _uiScope.Container.Resolve<PanelController>();
            InGamePanelController = _uiScope.Container.Resolve<InGamePanelController>();
            LevelEndDataPanel = _uiScope.Container.Resolve<LevelEndDataPanel>();
            _inventoryManager = _uiScope.Container.Resolve<InventoryManager>();
            _inventoryManager.PlayerTransform = _playerTransform;
        }
    }



}
