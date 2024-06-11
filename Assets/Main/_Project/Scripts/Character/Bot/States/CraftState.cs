
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Bot.Controllers;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;


namespace _Project.Scripts.Character.Bot.States
{
    public class CraftState : IState
    {
        [Inject] private BotAgentController _botAgentCotroller;
        [Inject] private BotAnimationController _botAnimController;
        [Inject] private BotUIMediator _botUIMediator;
        [Inject] private BotMovementController _botMovementController;
        private BotSM _botSM;
        public void Initalize(BotSM botSM)
        {
            _botSM = botSM;
        }
        public void Enter()
        {
            Debug.Log("CraftState");

            _botUIMediator.SetDebugImgColor(Color.blue);
            _botMovementController.FollowCraftable();
        }

        public void Exit()
        {
            _botUIMediator.SetDebugImgColor(Color.white);
        }

        public void FixedTick()
        {



        }

        public void Tick()
        {
            _botAgentCotroller.Update();
        }
    }
}