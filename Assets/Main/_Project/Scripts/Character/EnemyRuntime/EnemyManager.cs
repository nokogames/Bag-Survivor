using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Character.Runtime;
using Pack.GameData;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.EnemyRuntime
{

    public class EnemyManager : MonoBehaviour
    {
        [Inject] private EnemySpawnData _enemySpawnData;
        [Inject] private PlayerSM _playerSm;
        [Inject] private GameData _gameData;
        private Transform _playerTransform;
        private IDamagableByEnemy _damageableByEnemy;
        private List<Transform> _spawnedEnemys = new List<Transform>();
        private List<Enemy> _spawnedEnemyBehaviour = new List<Enemy>();
        private Transform[] _spawnPoints;

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
            var levelData = _enemySpawnData.EnemyLevelSpawnData(_gameData.CurrentLvl);
            var sectionData = levelData.EnmeySectionSpawnData(_gameData.CurrentSection);
            var waveInfos = sectionData.waveInfos;

            //  var waveInfos = _enemySpawnData.enemyLevelSpawnDatas[0].enmeySectionSpawnDatas[0].waveInfos;
            for (int i = 0; i < waveInfos.Count; i++)
            {
                var crrWave = waveInfos[i];
                Debug.LogWarning("..New Wave is coming ..." + crrWave.spawnEnemyInfos.Count);

                for (int j = 0; j < crrWave.spawnEnemyInfos.Count; j++)
                {
                    var typeOfType = crrWave.spawnEnemyInfos[j].enemyType;
                    CreateEnemy(typeOfType, crrWave.spawnEnemyInfos[j].Count);
                    // yield return new WaitForEndOfFrame();
                }
                if (crrWave.mustClearNextWave) yield return new WaitWhile(() => IsAliveEnemy());
                yield return new WaitForSeconds(crrWave.timeOffset);


            }
            // _gameData.CurrentSection++;

        }

        private bool IsAliveEnemy()
        {
            return _spawnedEnemys.Count > 0;
        }

        private void CreateEnemy(EnemyType typeOfType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = EnemyPool.SharedInstance.GetPooledObject(typeOfType);

                var enemyBehaviour = obj.GetComponent<Enemy>();
                _spawnedEnemyBehaviour.Add(enemyBehaviour);
                _spawnedEnemys.Add(obj.transform);
                UnityEngine.Random.InitState(i);
                obj.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position.GetRandomPositionAroundObject(1f);
                obj.SetActive(true);
                enemyBehaviour.Initialize(_playerTransform, this, _damageableByEnemy);
            }


        }

        public void EnmeyDead(Enemy enemy)
        {
            if (!_spawnedEnemyBehaviour.Contains(enemy)) return;
            _spawnedEnemyBehaviour.Remove(enemy);
            _spawnedEnemys.Remove(enemy.transform);
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
}