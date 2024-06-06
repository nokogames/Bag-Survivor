

using _Project.Scripts.Character.Runtime;
using _Project.Scripts.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class GameplayState : LifetimeScope
    {
        private bool _isValid;
        protected override void Awake()
        {
            base.Awake();

            if (Parent != null)
            {
                Parent.Container.Inject(this);
            }

        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInHierarchy<PlayerSM>();
            //  builder.RegisterComponentInHierarchy<PlayerController>();

            _isValid = true;
        }
        private void Start()
        {
            if (!_isValid) SceneManager.LoadScene("Startup");
        }
    }
}