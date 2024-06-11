using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Character.Bot.Controllers
{
    public class BotMovementController
    {
        [Inject] private ICharacter _character;
        [Inject] private BotAgentController _agentController;
        [Inject] private BotAnimationController _botAnimationController;

        public void FollowPlayer()
        {
            _agentController.SetDestination(_character.Transform.position);
        }
        public void FollowEnemy()
        {
           
            _agentController.FollowEnemy(_character.Target.Transform.position);
        }

        public void FollowCraftable()
        {
            
            _agentController.FollowCraftable(_character.Target.Transform.position);
        }


    }
}