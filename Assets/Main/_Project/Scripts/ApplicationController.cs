
using System.Collections.Generic;
using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Level;
using _Project.Scripts.Loader;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI;
using Pack.GameData;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class ApplicationController : LifetimeScope
    {
        [SerializeField] private LoaderMediator loaderMediatorPrefab;
        [SerializeField] private InputDataSO inputData;
        [SerializeField] private GameData gameData;
        [SerializeField] private EnemySpawnData enemySpawnData;
        [SerializeField] private PlayerUpgradeDatabase playerUpgradeDatabase;
        [SerializeField] private UIMediator uiMediatorPref;
        [SerializeField] private PlayerUpgradeDataSO playerRuntimeUpgradeData;
        [SerializeField] private List<SkillBase> Skills;
        [SerializeField] private ParticlePool particlePool;
        // private LoaderMediator _loaderMediator;
        private SceneLoader _sceneLoader;
        protected override void Awake()
        {
            gameData.Load();
            base.Awake();
            DontDestroyOnLoad(gameObject);


        }
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(loaderMediatorPrefab, Lifetime.Scoped).DontDestroyOnLoad();
            builder.RegisterComponentInNewPrefab(uiMediatorPref, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
            builder.RegisterComponentInNewPrefab(particlePool, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelDataManager>(Lifetime.Singleton).AsSelf();
            builder.RegisterInstance(gameData);
            builder.RegisterInstance(inputData);
            builder.RegisterInstance(enemySpawnData);
            builder.RegisterInstance(playerUpgradeDatabase);

            builder.RegisterInstance(playerRuntimeUpgradeData.savedPlayerData);
            builder.RegisterInstance(playerRuntimeUpgradeData.barData);
            builder.RegisterInstance(playerRuntimeUpgradeData.playerUpgradedData);
            builder.RegisterInstance(Skills).AsSelf();

            //Events
            builder.RegisterInstance(new InLevelEvents()).AsSelf().AsImplementedInterfaces();

        }
        private InLevelEvents _inLevelEvents;
        private void Start()
        {
            //  _loaderMediator = Container.Resolve<LoaderMediator>();
            _sceneLoader = Container.Resolve<SceneLoader>();
            Container.Resolve<ParticlePool>();
            _sceneLoader.LoadLevel("Level1");
            _inLevelEvents = Container.Resolve<InLevelEvents>();

            var uiM = Container.Resolve<UIMediator>();
            Application.targetFrameRate = 60;
#if !UNITY_EDITOR
            Debug.unityLogger.logEnabled=false;
            
#endif

        }
        private void Update()
        {
            #region  Debug
            if (Input.GetKeyDown(KeyCode.R)) _sceneLoader.LoadLevelWithSplash("Fighting");
            if (Input.GetKeyDown(KeyCode.E)) _sceneLoader.LoadLevelWithSplash("GamePlay");
            #endregion
        }
        protected override void OnDestroy()
        {
            if (_inLevelEvents != null) _inLevelEvents.Dispose();
            base.OnDestroy();

        }
    }
}