


using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Bot.Controllers;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;


namespace _Project.Scripts.Character.Bot.States
{
    public class AttackState : IState
    {
        [Inject] private BotAgentController _botAgentCotroller;
        [Inject] private BotAnimationController _botAnimController;
        [Inject] private BotUIMediator _botUIMediator;
        [Inject] private BotMovementController _botMovementController;
        [Inject] private BotGunController _botGunController;
        [Inject] private ICharacter _character;
        private BotSM _botSM;
        public void Initalize(BotSM botSM)
        {
            _botSM = botSM;
        }
        public void Enter()
        {
            Debug.Log("AttackState");
            _botUIMediator.SetDebugImgColor(Color.yellow);
            _botMovementController.FollowEnemy();
        }
        public void AfterEnter()
        {

        }
        public void Exit()
        {
            _botUIMediator.SetDebugImgColor(Color.white);
        }

        public void FixedTick()
        {

            _botGunController.FixedTick();
            _botMovementController.RotateToTarget();
            //Change
            if (_botMovementController.EnemyDistance() > 3)
            {
                _botMovementController.FollowEnemy();
            }


        }

        public void Tick()
        {
            _botAgentCotroller.Update();

        }
    }
}