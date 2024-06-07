using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Bot;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime
{
    public class PlayerSM : StateMachineMB, ICharacter
    {

        //PlayerController
        [SerializeField] private CharacterGraphics characterGraphics;
        [SerializeField] private BaseGunBehavior gunBehavior;
        [SerializeField] private EnemyDetector enemyDetector;
        [SerializeField] private List<Transform> botPlacePoints;
        private LifetimeScope _parentScope;
        private LifetimeScope _playerScope;

        //Controllers
        private PlayerMovementController _playerMovementController;
        private DetectionController _detectionController;
        private BotController _botController;
        private List<BotSM> _bots;

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

                builder.RegisterComponent(enemyDetector);
                builder.RegisterComponent(gunBehavior);
                builder.RegisterComponent(characterGraphics);
                builder.RegisterComponent(transform);
                builder.Register<DetectionController>(Lifetime.Scoped);
                builder.Register<PlayerMovementController>(Lifetime.Scoped);
                builder.Register<BotController>(Lifetime.Scoped);



            });
            RegisterBots();
        }

        private void RegisterBots()
        {
            _bots = new List<BotSM>();
            _bots = transform.FindComponentsInChild<BotSM>();
            _bots.ForEach(bot => bot.InjectDependenciesAndInitialize(_playerScope));

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
            _botController.Initialise(_bots, botPlacePoints);
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





        //About detection
        public IEnemy TargetEnemy => _detectionController.TargetEnemy;





        private void OnDestroy()
        {
            _playerScope.Dispose();
        }

        // public void AWAKE_TEST()
        // {
        //     Resolve();
        //     Initialize();
        // }
    }
}