using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Bot.States
{
    public class PlaceToPlayerState : IState
    {
        [Inject] private BotAgentController _botAgentCotroller;
        [Inject] private BotAnimationController _botAnimController;
        [Inject] private BotUIMediator _botUIMediator;

        private BotSM _botSM;
        // [Inject] private IBot _bot;
        public void Initalize(BotSM botSM)
        {
            _botSM = botSM;
        }
        public void Enter()
        { Debug.Log("PlaceToPlayerState");
            _botUIMediator.SetDebugImgColor(Color.red);
            _botAgentCotroller.SetAgentStatus(false);

            //  _botAnimController.SetActiveAnimation(false);
            _botAnimController.Place(OnCompletedPlace);
        }

        public void Exit()
        {

        }

        public void FixedTick()
        {

        }

        public void Tick()
        {


        }


        public void OnCompletedPlace()
        {
            //_botSM.ChangeState(_botSM.UnPlaceFromPlayerState);
        }
    }
}