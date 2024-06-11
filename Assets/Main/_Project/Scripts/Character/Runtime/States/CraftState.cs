

using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Reusable;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Runtime.States
{
    public class CraftState : IState
    {
        [Inject] private BaseGunBehavior _gunBehavior;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private CharacterGraphics _characterGraphics;
        [Inject] private AnimationEventHandler _animationEventHandler;
        [Inject] private PlayerAnimationController _playerAnimationController;
        [Inject] private BotController _botController;
        private GameObject _pickAxe;
        public void Initialize(GameObject pickAxe)
        {
            _animationEventHandler.OnCraft += Craft;
            _pickAxe = pickAxe;
        }
        public void Enter()
        {
            //Gun disable
            //axe enable
            _pickAxe.SetActive(true);
            _characterGraphics.DisableIK();
            _gunBehavior.SetActivity(false);
            _playerAnimationController.SetCraftStatus(true);
            _botController.CraftBots();

        }

        public void Exit()
        {
            _playerAnimationController.SetCraftStatus(false);
           
            _pickAxe.SetActive(false);

          //  _botController.PlaceBots();
        }

        public void FixedTick()
        {
        }

        public void Tick()
        {
            _gunBehavior.UpdateHandRig();
            _playerMovementController.Update();
        }

        internal void Craft()
        {
            Debug.Log("Crafted");
        }
    }
}