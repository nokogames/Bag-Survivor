using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Bot.States;
using _Project.Scripts.Reusable;

using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Bot
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotSM : StateMachineMB, IBot
    {
        [SerializeField] private Animator animator;


        private LifetimeScope _parentScope;
        private LifetimeScope _childScope;
        //States
        public IdleState IdleState { get; set; }
        public PlaceToPlayerState PlaceToPlayerState { get; set; }
        public AttackState AttackState { get; set; }
        public CraftState CraftState { get; set; }
        public UnPlaceFromPlayerState UnPlaceFromPlayerState { get; set; }
        public Transform PlayerPlacePoint { get => _playerPlacePoint; set => _playerPlacePoint = value; }

        public Transform Transform => transform;

        private Transform _playerPlacePoint;
        [Inject]
        public void InjectDependenciesAndInitialize(LifetimeScope parentScope)
        {
            _parentScope = parentScope;
            CreateBotScope();
        }


        private void CreateBotScope()
        {
            _childScope = _parentScope.CreateChild(builder =>
            {
                builder.RegisterInstance(GetComponent<IBot>());
                builder.RegisterComponent(GetComponent<NavMeshAgent>());
                builder.RegisterComponent(animator);

                builder.Register<BotAgentController>(Lifetime.Scoped);
                builder.Register<BotAnimationController>(Lifetime.Scoped);

                builder.Register<IdleState>(Lifetime.Scoped);
                builder.Register<PlaceToPlayerState>(Lifetime.Scoped);
                builder.Register<AttackState>(Lifetime.Scoped);
                builder.Register<CraftState>(Lifetime.Scoped);
                builder.Register<UnPlaceFromPlayerState>(Lifetime.Scoped);
            });
        }
        private void ResolveStates()
        {
            IdleState = _childScope.Container.Resolve<IdleState>();
            PlaceToPlayerState = _childScope.Container.Resolve<PlaceToPlayerState>();
            AttackState = _childScope.Container.Resolve<AttackState>();
            CraftState = _childScope.Container.Resolve<CraftState>();
            UnPlaceFromPlayerState = _childScope.Container.Resolve<UnPlaceFromPlayerState>();

        }
        private void InitializeStates()
        {
            IdleState.Initalize();
            PlaceToPlayerState.Initalize();
            AttackState.Initalize();
            CraftState.Initalize();
            UnPlaceFromPlayerState.Initalize();
        }

        private void Start()
        {
            ResolveStates();
            InitializeStates();
            ChangeState(PlaceToPlayerState);
        }
        public void Initialize(Transform playerPlacePoint)
        {
            _playerPlacePoint = playerPlacePoint;
        }


    }

}

/* 
NavMeshMovementMotor  =navmesh control etmek icin 
ADWalkerAnimator     = animasyon control icin 



*/
