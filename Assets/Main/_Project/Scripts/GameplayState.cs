

using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Character.EnemyRuntime;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Level;

using _Project.Scripts.SkillManagement;
using Pack.GameData;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;


namespace _Project.Scripts
{
    public class GameplayState : LifetimeScope
    {

        // [SerializeField] private PlayerRuntimeUpgradeData playerRuntimeUpgradeData;
        private bool _isValid;
        [Inject] private GameData _gameData;


        protected override void Awake()
        {

            base.Awake();
            if (Parent != null)
            {
                Parent.Container.Inject(this);
            }
            if (!_isValid) SceneManager.LoadScene("Startup");


            var savedPlayer = Container.Resolve<SavedPlayerData>();
            Container.Resolve<PlayerUpgradedData>().Reset();
            Container.Resolve<PlayerUpgradeDatabase>().Initialize(_gameData, savedPlayer);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            //base.Configure(builder);

            builder.RegisterComponentInHierarchy<PortalController>();
            builder.RegisterComponentInHierarchy<PlayerSM>();
            builder.RegisterComponentInHierarchy<EnemyManager>();
            builder.RegisterComponentInHierarchy<SkillManager>().AsSelf();
            builder.RegisterComponentInHierarchy<GemManager>();




            _isValid = true;


        }
        private void Start()
        {
            //  var savedPlayer = Container.Resolve<SavedPlayerData>();

            //Container.Resolve<PlayerUpgradeDatabase>().Initialize(_gameData, savedPlayer);

        }

    }
}