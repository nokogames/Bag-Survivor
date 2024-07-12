

using System;
using _Project.Scripts.Character.Runtime;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.SkillManagement.Controllers
{
    public class InGameSkillController
    {
        // [Inject] private PlayerSM _player;
        [Inject] private InGameSkillData _data;
        private Transform _playerTransform;
        public void FireBall(int ballCount)
        {
            Debug.Log("Selected FireBall Skill 2");
            CreateFireBall(ballCount);
        }
        public void Lightening(int count)
        {
            CreateLightening(count);
        }

        internal void Start(Transform transform)
        {
            _playerTransform = transform;
        }

        private void CreateFireBall(int count)
        {
            Debug.Log($"Selected FireBall Skill 3---{count}");
            for (int i = 0; i < count; i++)
            {
                var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(_data.fireballPrefab).GetComponent<FireBallBehavior>();
                fireBallBehavior.Initialise(_playerTransform);
                fireBallBehavior.gameObject.SetActive(true);

            }
        }
        private void CreateLightening(int count)
        {
            Debug.Log($"Selected Lightening Skill 3---{count}");
            for (int i = 0; i < count; i++)
            {
                var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(_data.lighteningPrefab).GetComponent<LighteningSkillBehaviour>();
                fireBallBehavior.Initialise(_playerTransform);
                fireBallBehavior.gameObject.SetActive(true);

            }
        }
    }
    [Serializable]
    public class InGameSkillData
    {
        public GameObject fireballPrefab;
        public GameObject lighteningPrefab;
    }
}