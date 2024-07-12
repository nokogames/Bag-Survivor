using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Bot.Controllers
{
    public class BotMovementController
    {
        [Inject] private ICharacter _character;
        [Inject] private BotAgentController _agentController;
        [Inject] private BotAnimationController _botAnimationController;
        [Inject] private IBot _bot;

        public void FollowPlayer()
        {
            _agentController.FollowPlayer(_character.Transform.position);
        }
        public void FollowEnemy()
        {
            // if (_character.Target == null) return;
            _agentController.FollowEnemy(_character.Target.Transform.position);
        }

        public void FollowCraftable()
        {
            // if (_character.Target == null) return;
            _agentController.FollowCraftable(_character.Target.Transform.position);
        }

        public void RotateToTarget()
        {
            // if (_character.Target == null) return;



            Vector3 targetDirection = _character.Target.Transform.position - _bot.Transform.position;
            targetDirection.y = 0;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                _bot.Transform.rotation = Quaternion.Lerp(_bot.Transform.rotation, targetRotation, 2f * Time.fixedDeltaTime);

            }
        }

        internal float EnemyDistance()
        {     
            return _agentController.Distance(_character.Target.Transform.position) + .1f;
        }
    }
}