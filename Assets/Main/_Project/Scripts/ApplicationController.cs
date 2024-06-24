
using _Project.Scripts.Loader;
using _Project.Scripts.UI;
using Pack.GameData;
using ScriptableObjects;
using Unity.VisualScripting;
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
       
        // private LoaderMediator _loaderMediator;
        private SceneLoader _sceneLoader;
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(loaderMediatorPrefab, Lifetime.Scoped).DontDestroyOnLoad();
            builder.RegisterComponentInHierarchy<UIMediator>().DontDestroyOnLoad();
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterInstance(gameData);
            builder.RegisterInstance(inputData);
            builder.RegisterInstance(enemySpawnData);

        }

        private void Start()
        {
            //  _loaderMediator = Container.Resolve<LoaderMediator>();
            _sceneLoader = Container.Resolve<SceneLoader>();
            _sceneLoader.LoadLevel("GamePlay");
        }
        private void Update()
        {
            #region  Debug
            if (Input.GetKeyDown(KeyCode.R)) _sceneLoader.LoadLevelWithSplash("Fighting");
            if (Input.GetKeyDown(KeyCode.E)) _sceneLoader.LoadLevelWithSplash("GamePlay");
            #endregion
        }

    }
}