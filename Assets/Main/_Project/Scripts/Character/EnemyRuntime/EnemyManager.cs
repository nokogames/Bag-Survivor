using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.EnemyRuntime
{

    public class EnemyManager : MonoBehaviour
    {
        [Inject] private EnemySpawnData _enemySpawnData;
        private int _startLevel = 0;
        private int _startSection = 0;
        private int _startWave = 0;

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
                obj.SetActive(true);
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
