using System;
using System.Collections.Generic;

using _Project.Scripts.Character.Craft;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Character.Runtime.States;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime
{
    public class PlayerSM : StateMachineMB, ICharacter
    {

        //PlayerController
        [SerializeField] private CraftDetector craftDetector;
        [SerializeField] private CharacterGraphics characterGraphics;
        [SerializeField] private BaseGunBehavior gunBehavior;
        [SerializeField] private EnemyDetector enemyDetector;
        [SerializeField] private List<Transform> botPlacePoints;
        [SerializeField] private GameObject botPrefab;
        private LifetimeScope _parentScope;
        private LifetimeScope _playerScope;

        //Controllers
        private PlayerMovementController _playerMovementController;
        private DetectionController _detectionController;
        private BotController _botController;


        //States
        public IdleState IdleState { get; set; }
        public CraftState CraftState { get; set; }

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

                builder.RegisterInstance(GetComponent<ICharacter>());
                builder.RegisterComponent(enemyDetector);
                builder.RegisterComponent(gunBehavior);
                builder.RegisterComponent(characterGraphics);
                builder.RegisterComponent(transform);
                builder.RegisterComponent(craftDetector);
                builder.Register<BotController>(Lifetime.Scoped);
                builder.Register<DetectionController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
                builder.Register<PlayerMovementController>(Lifetime.Scoped);
                builder.Register<BotController>(Lifetime.Scoped);
                //States
                builder.Register<IdleState>(Lifetime.Scoped);
                builder.Register<CraftState>(Lifetime.Scoped);


            });

        }



        private void Awake()
        {
            Resolve();
            Initialize();
        }

        private void Initialize()
        {

            characterGraphics.Initialise(this);
            gunBehavior.InitialiseCharacter(characterGraphics, this);
            enemyDetector.Initialise(_detectionController);
            _playerMovementController.Initialise();
            _botController.Initialise(botPrefab, botPlacePoints);
            _detectionController.Initialise(this);
        }

        private void Resolve()
        {
            _playerMovementController = _playerScope.Container.Resolve<PlayerMovementController>();
            _detectionController = _playerScope.Container.Resolve<DetectionController>();
            _botController = _playerScope.Container.Resolve<BotController>();
            //States
            IdleState = _playerScope.Container.Resolve<IdleState>();
            CraftState = _playerScope.Container.Resolve<CraftState>();
        }


        //About detection
        public IEnemy TargetEnemy => _detectionController.TargetEnemy;



        private void Start()
        {
            ChangeState(IdleState);
        }

        private void OnDestroy()
        {
            _playerScope.Dispose();
        }


    }
}