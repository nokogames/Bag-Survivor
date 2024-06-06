using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime
{
    public class PlayerSM : StateMachineMB, ICharacter
    {
        private LifetimeScope _parentScope;
        private LifetimeScope _playerScope;
        //PlayerController
        [SerializeField] private CharacterGraphics characterGraphics;
        [SerializeField] private BaseGunBehavior gunBehavior;
        [SerializeField] private EnemyDetector enemyDetector;


        //Controllers
        private PlayerMovementController _playerMovementController;
        private DetectionController _detectionController;



        [Inject]
        void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            CreatePlayerScope();
        }

        private void CreatePlayerScope()
        {
            _playerScope = _parentScope.CreateChild(builder =>
            {

                builder.RegisterComponent(enemyDetector);
                builder.RegisterComponent(gunBehavior);
                builder.RegisterComponent(characterGraphics);
                builder.RegisterComponent(transform);
                builder.Register<DetectionController>(Lifetime.Scoped);

                builder.Register<PlayerMovementController>(Lifetime.Scoped);


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
        }

        private void Resolve()
        {
            _playerMovementController = _playerScope.Container.Resolve<PlayerMovementController>();
            _detectionController = _playerScope.Container.Resolve<DetectionController>();
        }




        public override void Update()
        {
            base.Update();
            gunBehavior.UpdateHandRig();
            _playerMovementController.Update();
        }


        private void Start()
        {

        }


        //About detection
        public IEnemy TargetEnemy => _detectionController.TargetEnemy;



        public new bool TryGetComponent<T>(out T component)
        {
            if (typeof(T) == typeof(IEnemyDetector))
            {

                Debug.Log("ddd");
                component = (T)(object)_detectionController;
                return true;
            }

            return gameObject.TryGetComponent(out component);
        }

        private void OnDestroy()
        {
            _playerScope.Dispose();
        }
    }
}