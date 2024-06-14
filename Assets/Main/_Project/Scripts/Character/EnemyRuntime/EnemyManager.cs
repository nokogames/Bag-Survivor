using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Character.Runtime;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.EnemyRuntime
{

    public class EnemyManager : MonoBehaviour
    {
        [Inject] private EnemySpawnData _enemySpawnData;
        [Inject] private PlayerSM _playerSm;
        private Transform _playerTransform;
        private IDamagableByEnemy _damageableByEnemy;
        private List<Transform> _spawnedEnemys = new List<Transform>();
        private List<Enemy> _spawnedEnemyBehaviour = new List<Enemy>();
        private Transform[] _spawnPoints;
        private int _startLevel = 0;
        private int _startSection = 0;
        private int _startWave = 0;

        private void Awake()
        {
            _spawnPoints = gameObject.GetComponentsInChildren<Transform>(true).Where(t => t != transform).ToArray();
            
            _playerTransform = _playerSm.Transform;
            _damageableByEnemy = _playerSm;
        }
        private void Start()
        {  //Debug
            GameStarted();
        }
        public void GameStarted()
        {
            StartCoroutine(SpawnEnemys());
        }
        IEnumerator SpawnEnemys()
        {
            var waveInfos = _enemySpawnData.enemyLevelSpawnDatas[0].enmeySectionSpawnDatas[0].waveInfos;
            for (int i = 0; i < waveInfos.Count; i++)
            {
                var crrWave = waveInfos[i];
                for (int j = 0; j < crrWave.spawnEnemyInfos.Count; j++)
                {
                    var typeOfType = crrWave.spawnEnemyInfos[j].enemyType;
                    CreateEnemy(typeOfType, crrWave.spawnEnemyInfos[j].Count);
                }

            }

            yield return null;
        }

        private void CreateEnemy(EnemyType typeOfType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = EnemyPool.SharedInstance.GetPooledObject(typeOfType);

                var enemyBehaviour = obj.AddComponent<Enemy>();
                enemyBehaviour.Initialize(_playerTransform, this, _damageableByEnemy);
                _spawnedEnemyBehaviour.Add(enemyBehaviour);
                _spawnedEnemys.Add(obj.transform);
                obj.SetActive(true);
                obj.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position.GetRandomPositionAroundObject(1f);
            }
        }
    }
    public enum EnemyType
    {
        Level1,
        Level2,
        Level3,
        Boss1,
        Boss2,
        Boss3,


    }
}
