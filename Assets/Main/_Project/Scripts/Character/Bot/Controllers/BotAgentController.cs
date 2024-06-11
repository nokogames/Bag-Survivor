using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
namespace _Project.Scripts.Character.Bot
{
    public class BotAgentController
    {
        [Inject] private NavMeshAgent _agent;
        [Inject] private BotAnimationController _botAnimController;
        private float _defaultStopDistance;
        private float _enemyStopDistance = 2f;
        private float _craftableStopDistance = 1f;
        private float proximityThreshold = 1.0f;

        //public Action OnTargetReached;
        public void Initialise()
        {
            _defaultStopDistance = _agent.stoppingDistance;


        }

        public void SetAgentStatus(bool status)
        {
            _agent.enabled = status;
        }

        internal void SetDestination(Vector3 position)
        {
            _agent.stoppingDistance = _defaultStopDistance;
            _agent.SetDestination(position);
        }
        internal void FollowEnemy(Vector3 position)
        {
            _agent.stoppingDistance = _defaultStopDistance + _enemyStopDistance;
            _agent.SetDestination(position);
        }
        internal void FollowCraftable(Vector3 position)
        {
            _agent.stoppingDistance = _defaultStopDistance + _craftableStopDistance;
            _agent.SetDestination(position);
        }
        public bool IsReachDestination(float offset = 0)
        {
            return _agent.remainingDistance <= _agent.stoppingDistance + offset && !_agent.pathPending;
        }


        public void Update()
        {

            if (!_agent.pathPending && _agent.remainingDistance <= proximityThreshold)
            {
                if (_agent.hasPath && _agent.remainingDistance == 0)
                {
                    _botAnimController.Stop();


                }


            }
            else if (_agent.remainingDistance > _agent.stoppingDistance && _agent.hasPath && !_agent.pathPending)
            {
                _botAnimController.Move();
            }

        }
    }
}
