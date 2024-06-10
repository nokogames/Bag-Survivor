using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Reusable;
using VContainer;

namespace _Project.Scripts.Character.Runtime.States
{
    public class IdleState : IState
    {
        [Inject] private BaseGunBehavior _gunBehavior;
        [Inject] private PlayerMovementController _playerMovementController;

        public void Enter()
        {

        }

        public void Exit()
        {
        }

        public void FixedTick()
        {
        }

        public void Tick()
        {
            _gunBehavior.UpdateHandRig();
            _playerMovementController.Update();
        }
    }
}