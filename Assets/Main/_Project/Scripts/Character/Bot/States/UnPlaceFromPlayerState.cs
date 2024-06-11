using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Bot.States
{
    public class UnPlaceFromPlayerState : IState
    {
        [Inject] private BotAgentController _botAgentCotroller;
        [Inject] private BotAnimationController _botAnimController;
        [Inject] private BotUIMediator _botUIMediator;
        public IState AfterState { get; set; }
        public Action onUnplace;
        private BotSM _botSM;
        public void Initalize(BotSM botSM)
        {
            _botSM = botSM;
        }
        public void Enter()
        {
            _botUIMediator.SetDebugImgColor(Color.green);
            Debug.Log("Unplace Enter");
            _botAgentCotroller.SetAgentStatus(false);

            //    _botAnimController.SetActiveAnimation(true);
            _botAnimController.UnPlace(OnCompletedUnPlace);
        }

        public void Exit()
        {
            _botUIMediator.SetDebugImgColor(Color.white);
            onUnplace = null;
        }

        public void FixedTick()
        {

        }

        public void Tick()
        {

        }
        public void OnCompletedUnPlace()
        {
            _botAgentCotroller.SetAgentStatus(true);
            //_botSM.ChangeState(_botSM.PlaceToPlayerState);
            onUnplace?.Invoke();
            onUnplace = null;
            if (AfterState != null)
            {
                _botSM.ChangeState(AfterState);
            }
        }
    }
}