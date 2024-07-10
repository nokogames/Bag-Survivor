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
        [Inject] private IBot _bot;
        private float _defaultStopDistance;
        private float _enemyStopDistance = 2f;
        private float _playerStopDistance = 2.5f;
        private float _craftableStopDistance = 1f;
        private float proximityThreshold = .1f;

        //public Action OnTargetReached;
        public void Initialise()
        {
            _defaultStopDistance = _agent.stoppingDistance;


        }

        public void SetAgentStatus(bool status)
        {
            _agent.enabled = status;
        }
        internal void FollowPlayer(Vector3 position)
        {
            if (!_agent.enabled) return;
            //if (!_agent.pathPending) return;
            _agent.stoppingDistance = _playerStopDistance;
            _agent.SetDestination(position);
        }
        internal void SetDestination(Vector3 position)
        {
            _agent.stoppingDistance = _defaultStopDistance;
            _agent.SetDestination(position);
        }
        internal void FollowEnemy(Vector3 position)
        {
            if (!_agent.enabled) return;
            _agent.stoppingDistance = _defaultStopDistance + _enemyStopDistance;
            _agent.SetDestination(position);
        }
        internal void FollowCraftable(Vector3 position)
        {
            if (!_agent.enabled) return;
            _agent.stoppingDistance = _defaultStopDistance + _craftableStopDistance;
            _agent.SetDestination(position);
        }
        public bool IsReachDestination(float offset = 0)
        {
            if (!_agent.enabled) return false;
            return _agent.remainingDistance <= _agent.stoppingDistance + offset && !_agent.pathPending;
        }

        public void SetSpeed(float speed)
        {
              _agent.speed=speed;
        }
        public void Update()
        {

            if (IsReachDestination(proximityThreshold))
            {
                _botAnimController.Stop();

                // Hedefe ulaşıldığında animasyonu durdur
            }
            else
            {
                _botAnimController.Move();  // Hedefe henüz ulaşılmadıysa hareket animasyonunu sürdür
            }

        }

        internal float Distance(Vector3 position)
        {
            return Vector3.Distance(position, _bot.Transform.position);
        }
    }
}
