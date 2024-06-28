

using _Project.Scripts.Character.Datas;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Character.EnemyRuntime;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Loader;
using _Project.Scripts.SkillManagement;
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
        protected override void Awake()
        {

            base.Awake();
            if (Parent != null)
            {
                Parent.Container.Inject(this);
            }
            if (!_isValid) SceneManager.LoadScene("Startup");
        }

        protected override void Configure(IContainerBuilder builder)
        {
            //base.Configure(builder);

            builder.RegisterComponentInHierarchy<PlayerSM>();
            builder.RegisterComponentInHierarchy<EnemyManager>();
            builder.RegisterComponentInHierarchy<SkillManager>().AsSelf();
            // builder.RegisterInstance(playerRuntimeUpgradeData.runTimePlayerData);


            _isValid = true;

        }
        private void Start()
        {    
             Container.Resolve<SavedPlayerData>();
             Container.Resolve<PlayerUpgradedData>().Reset();

            
        }
    }
}