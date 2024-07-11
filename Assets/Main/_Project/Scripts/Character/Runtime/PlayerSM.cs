using System;
using System.Collections.Generic;

using _Project.Scripts.Character.Craft;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Character.Runtime.SerializeData;
using _Project.Scripts.Character.Runtime.States;
using _Project.Scripts.Interactable.Collectable;
using _Project.Scripts.Interactable.Craft;
using _Project.Scripts.Level;
using _Project.Scripts.Reusable;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime
{
    public class PlayerSM : StateMachineMB, ICharacter, IDamagableByEnemy
    {
        [Header("UI Elements"), SerializeField] private PlayerUIData playerUIData;
        //PlayerController
        [Header("Components"), SerializeField] private CollectableDetector collectableDetector;
        [SerializeField] private CraftDetector craftDetector;
        [SerializeField] private CharacterGraphics characterGraphics;
        [SerializeField] private BaseGunBehavior gunBehavior;
        [SerializeField] private EnemyDetector enemyDetector;
        [SerializeField] private List<Transform> botPlacePoints;
        [SerializeField] private GameObject botPrefab;
        [SerializeField] private AnimationEventHandler animationEventHandler;
        [SerializeField] private GameObject pickAxe;
        private LifetimeScope _parentScope;
        private LifetimeScope _playerScope;
        private SkillManager _skillManager;
        private UIMediator _uiMediator;
        //Controllers
        private PlayerMovementController _playerMovementController;
        private DetectionController _detectionController;
        private BotController _botController;
        private HealthController _healthController;


        //States
        public IdleState IdleState { get; set; }
        public CraftState CraftState { get; set; }
        public AttackState AttackState { get; set; }
        public DiedState DiedState { get; set; }


        private PlayerAnimationController _playerAnimationController;
        private InLevelEvents _inLevelEvents;

        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope, SkillManager skillManager, UIMediator uIMediator, InLevelEvents inlevelEvents)
        {
            _inLevelEvents = inlevelEvents;
            _skillManager = skillManager;
            _parentScope = parentScope;
            _uiMediator = uIMediator;
            // var result = parentScope.Container.Resolve<UIMediator>();
            CreatePlayerScope();
        }

        private void CreatePlayerScope()
        {


            _playerScope = _parentScope.CreateChild(builder =>
            {

                builder.RegisterInstance(GetComponent<ICharacter>());
                builder.RegisterInstance(GetComponent<CharacterController>());
                builder.RegisterComponent(enemyDetector);
                builder.RegisterComponent(gunBehavior);
                builder.RegisterComponent(transform);
                builder.RegisterComponent(craftDetector);
                builder.RegisterComponent(characterGraphics);
                builder.RegisterComponent(animationEventHandler);
                builder.RegisterComponent(collectableDetector);
                builder.RegisterComponent(playerUIData);

                builder.Register<BotController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<DetectionController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
                builder.Register<PlayerMovementController>(Lifetime.Scoped);
                builder.Register<BotController>(Lifetime.Scoped);
                builder.Register<PlayerAnimationController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<HealthController>(Lifetime.Scoped).AsSelf();
                
                builder.Register(_ => _skillManager.BarController, Lifetime.Scoped);
                builder.Register(_ => _uiMediator.PanelController, Lifetime.Scoped);
                builder.Register(_ => _uiMediator.InGamePanelController, Lifetime.Scoped);

                builder.RegisterEntryPoint<UpgradeDataApplyer>(Lifetime.Singleton);

                //States
                builder.Register<IdleState>(Lifetime.Scoped);
                builder.Register<CraftState>(Lifetime.Scoped);
                builder.Register<AttackState>(Lifetime.Scoped);
                builder.Register<DiedState>(Lifetime.Scoped);


            });
            //_playerScope.Container.Resolve<UpgradeDataApplyer>();
        }



        private void Awake()
        {
            Resolve();
            Initialize();
        }
        private void OnEnable()
        {
            _inLevelEvents.onNextLevel += OnLevelStart;
            _inLevelEvents.onNextSection += OnSectionStart;

        }



        private void OnDisable()
        {
            _inLevelEvents.onNextLevel -= OnLevelStart;
            _inLevelEvents.onNextSection -= OnSectionStart;

        }
        public void OnLevelStart()
        {
            //Reset Some
            _healthController.ResetHealth();
            ChangeState(IdleState);
        }
        private void OnSectionStart()
        {
            _healthController.ResetHealth();
        }
        private void Initialize()
        {

            characterGraphics.Initialise(this);
            gunBehavior.InitialiseCharacter(characterGraphics, this);
            enemyDetector.Initialise(_detectionController);
            collectableDetector.Initialise(_detectionController);
            craftDetector.Initialise(_detectionController);

            _playerMovementController.Initialise();
            _botController.Initialise(botPrefab, botPlacePoints);
            _detectionController.Initialise(this);
            _healthController.Initialise(this);
            //States
            IdleState.Initialize();
            CraftState.Initialize(pickAxe);
            AttackState.Initialize();


        }

        private void Resolve()
        {
            _playerMovementController = _playerScope.Container.Resolve<PlayerMovementController>();
            _detectionController = _playerScope.Container.Resolve<DetectionController>();
            _botController = _playerScope.Container.Resolve<BotController>();
            //States
            IdleState = _playerScope.Container.Resolve<IdleState>();
            CraftState = _playerScope.Container.Resolve<CraftState>();
            AttackState = _playerScope.Container.Resolve<AttackState>();
            DiedState = _playerScope.Container.Resolve<DiedState>();

            _playerAnimationController = _playerScope.Container.Resolve<PlayerAnimationController>();
            _healthController = _playerScope.Container.Resolve<HealthController>();
        }


        //About detection
        public ITargetable Target => _detectionController.Target;
        public ICraftable Craftable => _detectionController.Craftable;

        public Transform Transform => transform;

        public bool IsEnemyFound => _detectionController.IsEnemyFound;

        private void Start()
        {

            ChangeState(IdleState);
        }

        private void OnDestroy()
        {
            _playerScope.Dispose();
        }
        public void OnGunShooted()
        {
            _playerAnimationController.OnGunShooted();
        }

        public void GetDamage(float damage)
        {
            _healthController.GetDamage(damage);
        }



    }


}