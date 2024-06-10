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
        // [Inject] private IBot _bot;
        public void Initalize()
        {

        }
        public void Enter()
        {
            _botAnimController.SetActiveAnimation(false);
            _botAnimController.Place();
            _botAgentCotroller.SetAgentStatus(false);
        }

        public void Exit()
        {

        }

        public void FixedTick()
        {

        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) _botAnimController.Place();
            if (Input.GetKeyDown(KeyCode.Alpha2)) _botAnimController.UnPlace();

        }
    }
}