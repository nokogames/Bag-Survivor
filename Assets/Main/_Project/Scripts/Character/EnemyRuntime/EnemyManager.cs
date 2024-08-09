using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Level;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
using Cysharp.Threading.Tasks;
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
        [Inject] private LevelDataManager _levelDataManager;

        private Transform _playerTransform;
        private IDamagableByEnemy _damageableByEnemy;
        private List<Transform> _spawnedEnemys = new List<Transform>();
        private List<Enemy> _spawnedEnemyBehaviour = new List<Enemy>();
        private Transform[] _spawnPoints;
        private SectionUIController _sectionUIController;

        private EnemyLevelSpawnData _levelData;
        private Coroutine _spawnCoroutine;
        private void Awake()
        {
            _sectionUIController = _uiMediator._uiScope.Container.Resolve<SectionUIController>();
            _spawnPoints = gameObject.GetComponentsInChildren<Transform>(true).Where(t => t != transform).ToArray();

            _playerTransform = _playerSm.Transform;
            _damageableByEnemy = _playerSm;
            _playerSm.EnemyManager = this;


            _inLevelEvents.onNextSection += NextSection;
            _inLevelEvents.onNextLevel += OnNextLevel;

            _levelDataManager.EnemyManager = this;
        }


        private void OnDestroy()
        {
            _inLevelEvents.onNextSection -= NextSection;
            _inLevelEvents.onNextLevel -= OnNextLevel;

        }

        private void OnNextLevel()
        {
            _clearedEnemyCount = 0;
            _allEnemyCountInLevel = _enemySpawnData.EnemyLevelSpawnData(_gameData.CurrentLvl).AllEnemyCount();
            _gameData.CurrentSection = 0;
            _spawnCoroutine = StartCoroutine(SpawnEnemys());

        }

        private void Start()
        {  //Debug
            _gameData.CurrentSection = 0;
            _sectionUIController.SetLevelTxt(_gameData.CurrentLvl + 1);
            // GameStarted();
        }
        // public void GameStarted()
        // {
        //     _spawnCoroutine = StartCoroutine(SpawnEnemys());
        // }

        private int _allEnemyCountInLevel;
        private float _clearedEnemyCount;

        public float ClearedPercentage()
        {
            return (_clearedEnemyCount / _allEnemyCountInLevel) * 100;
        }
        public float EarnedCoin()
        {
            return _levelData.TotalCoinCount * (_clearedEnemyCount / _allEnemyCountInLevel);
        }
        // private void FixedUpdate()
        // {
        //     Debug.Log($"Percentage -{ClearedPercentage()}");
        // }
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
              

                for (int j = 0; j < crrWave.spawnEnemyInfos.Count; j++)
                {
                    var typeOfType = crrWave.spawnEnemyInfos[j].enemyType;

                    CreateEnemy(typeOfType, crrWave.spawnEnemyInfos[j].Count).Forget();

                    yield return new WaitForSeconds(0.1f);
                }
                if (crrWave.mustClearNextWave) yield return new WaitWhile(() => IsAliveEnemy());
                //   yield return new WaitWhile(() => StaticHelper.Instance.gameStatus==GameStatus.Pause);
                yield return new WaitForSeconds(crrWave.timeOffset);


            }

            // else
            // {
            //     Debug.Log("New section is coming...");

            //     yield return new WaitForSeconds(sectionData.timeOffset);
            //     yield return new WaitWhile(() => _boseEnemy != null);

            // }

        }



        private bool IsAliveEnemy()
        {
            return _spawnedEnemys.Count > 0;
        }
        public void PlayerDied()
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            _spawnedEnemyBehaviour.Clear();
            _spawnedEnemys.ForEach(t => t.gameObject.SetActive(false));
            _spawnedEnemys.Clear();
            _gameData.CurrentSection = 0;


        }
        private async UniTaskVoid CreateEnemy(EnemyType typeOfType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = EnemyPool.SharedInstance.GetPooledObject(typeOfType);

                var enemyBehaviour = obj.GetComponent<Enemy>();


                _spawnedEnemyBehaviour.Add(enemyBehaviour);
                _spawnedEnemys.Add(obj.transform);
                //  UnityEngine.Random.InitState(i);
                var spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
                obj.transform.position = spawnPoint.position.GetRandomPositionAroundObject(2f);
                obj.SetActive(true);
                obj.transform.position = obj.transform.position.SetY(0.05f);
                enemyBehaviour.Initialize(_playerTransform, this, _damageableByEnemy);
                await UniTask.DelayFrame(1);
            }


        }



        public void EnmeyDead(Enemy enemy)
        {
            if (!_spawnedEnemyBehaviour.Contains(enemy)) return;
            _clearedEnemyCount++;

            _spawnedEnemyBehaviour.Remove(enemy);
            _spawnedEnemys.Remove(enemy.transform);
            if (enemy == IsTypeBose(enemy.EnemyType))
            {
                Debug.Log("Bose is dead...");

                _spawnedEnemyBehaviour.ForEach(x => x.CompletedSection());
                _spawnedEnemys.Clear();
                _spawnedEnemyBehaviour.Clear();
                if (_levelData.IsCompletedSections(_gameData.CurrentSection + 1)) _inLevelEvents.onAbleToNextLevel?.Invoke();
                else
                {
                    _inLevelEvents.onAbleToNextSection?.Invoke();
                    //Show next section ui
                    //Clear all enemies
                }
            }
        }


        // public void StartNewSection()
        // {
        //     _gameData.CurrentSection++;
        //     StartCoroutine(SpawnEnemys());
        // }

        private void NextSection()
        {
            //Controle level 
            _gameData.CurrentSection++;
            StartCoroutine(SpawnEnemys());
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