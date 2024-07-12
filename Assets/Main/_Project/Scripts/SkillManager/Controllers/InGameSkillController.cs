

using System;
using _Project.Scripts.Character.Runtime;
using Cysharp.Threading.Tasks;
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
            CreateFireBall(ballCount).Forget();
        }
        public void Lightening(int count)
        {
            CreateLightening(count).Forget();
        }

        internal void Start(Transform transform)
        {   
          
            _playerTransform = transform;
        }

        private async UniTaskVoid CreateFireBall(int count)
        {

            for (int i = 0; i < count; i++)
            {
                var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(_data.fireballPrefab).GetComponent<FireBallBehavior>();
                fireBallBehavior.Initialise(_playerTransform);
                fireBallBehavior.gameObject.SetActive(true);
                await UniTask.Delay(UnityEngine.Random.Range(0, 500));
            }
        }
        private async UniTaskVoid CreateLightening(int count)
        {

            for (int i = 0; i < count; i++)
            {
                var fireBallBehavior = ParticlePool.SharedInstance.GetPooledObject(_data.lighteningPrefab).GetComponent<LighteningSkillBehaviour>();
                fireBallBehavior.Initialise(_playerTransform);
                fireBallBehavior.gameObject.SetActive(true);
                await UniTask.Delay(UnityEngine.Random.Range(0, 700));

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