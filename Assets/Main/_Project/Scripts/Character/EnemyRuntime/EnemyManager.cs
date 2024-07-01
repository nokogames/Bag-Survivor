using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Level;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
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
        [Inject] private UIMediator _uiMediator;
        [Inject] private InLevelEvents _inLevelEvents;
        private Transform _playerTransform;
        private IDamagableByEnemy _damageableByEnemy;
        private List<Transform> _spawnedEnemys = new List<Transform>();
        private List<Enemy> _spawnedEnemyBehaviour = new List<Enemy>();
        private Transform[] _spawnPoints;
        private SectionUIController _sectionUIController;
        private Enemy _boseEnemy;
        private EnemyLevelSpawnData _levelData;
        private void Awake()
        {
            _sectionUIController = _uiMediator._uiScope.Container.Resolve<SectionUIController>();
            _spawnPoints = gameObject.GetComponentsInChildren<Transform>(true).Where(t => t != transform).ToArray();

            _playerTransform = _playerSm.Transform;
            _damageableByEnemy = _playerSm;
        }

        private void OnEnable()
        {

            _inLevelEvents.onNextSection += NextSection;
        }

        private void OnDisable()
        {
            _inLevelEvents.onNextSection -= NextSection;

        }
        private void Start()
        {  //Debug
            _gameData.CurrentSection = 0;
            _sectionUIController.SetLevelTxt(_gameData.CurrentLvl + 1);
            GameStarted();
        }
        public void GameStarted()
        {
            StartCoroutine(SpawnEnemys());
        }
        IEnumerator SpawnEnemys()
        {
            _levelData = _enemySpawnData.EnemyLevelSpawnData(_gameData.CurrentLvl);
            var sectionData = _levelData.EnmeySectionSpawnData(_gameData.CurrentSection);
            var waveInfos = sectionData.waveInfos;
            _sectionUIController.SetSectionTxt(_gameData.CurrentSection + 1);

            //  var waveInfos = _enemySpawnData.enemyLevelSpawnDatas[0].enmeySectionSpawnDatas[0].waveInfos;
            for (int i = 0; i < waveInfos.Count; i++)
            {
                var crrWave = waveInfos[i];
                Debug.LogWarning("..New Wave is coming ..." + crrWave.spawnEnemyInfos.Count);

                for (int j = 0; j < crrWave.spawnEnemyInfos.Count; j++)
                {
                    var typeOfType = crrWave.spawnEnemyInfos[j].enemyType;

                    CreateEnemy(typeOfType, crrWave.spawnEnemyInfos[j].Count);
                    yield return null;
                    // yield return new WaitForEndOfFrame();
                }
                if (crrWave.mustClearNextWave) yield return new WaitWhile(() => IsAliveEnemy());
                yield return new WaitForSeconds(crrWave.timeOffset);


            }

            // else
            // {
            //     Debug.Log("New section is coming...");

            //     yield return new WaitForSeconds(sectionData.timeOffset);
            //     yield return new WaitWhile(() => _boseEnemy != null);

            // }

        }

        private void NextLevel()
        {
            Debug.LogWarning("..Next Level is coming...");
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
                if (IsTypeBose(typeOfType)) _boseEnemy = enemyBehaviour;

                _spawnedEnemyBehaviour.Add(enemyBehaviour);
                _spawnedEnemys.Add(obj.transform);
                UnityEngine.Random.InitState(i);
                obj.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position.GetRandomPositionAroundObject(1f);
                obj.SetActive(true);
                obj.transform.position = obj.transform.position.SetY(-0.37f);
                enemyBehaviour.Initialize(_playerTransform, this, _damageableByEnemy);
            }


        }



        public void EnmeyDead(Enemy enemy)
        {
            if (!_spawnedEnemyBehaviour.Contains(enemy)) return;
            _spawnedEnemyBehaviour.Remove(enemy);
            _spawnedEnemys.Remove(enemy.transform);
            if (enemy == IsTypeBose(enemy.EnemyType))
            {
                Debug.Log("Bose is dead...");
                _boseEnemy = null;
                if (_levelData.IsCompletedSections(_gameData.CurrentSection + 1)) NextLevel();
                else
                {
                    _spawnedEnemyBehaviour.ForEach(x => x.CompletedSection());
                    _spawnedEnemys.Clear();
                    _spawnedEnemyBehaviour.Clear();
                    _inLevelEvents.onAbleToNextSection?.Invoke();
                    //Show next section ui
                    //Clear all enemies
                }
            }
        }


        public void StartNewSection()
        {
            _gameData.CurrentSection++;
            StartCoroutine(SpawnEnemys());
        }

        private void NextSection()
        {
            //Controle level 
            StartNewSection();
        }

        private bool IsTypeBose(EnemyType typeOfType)
        {
            return typeOfType == EnemyType.Boss1 || typeOfType == EnemyType.Boss2 || typeOfType == EnemyType.Boss3;
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