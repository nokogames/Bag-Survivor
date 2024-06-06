
using _Project.Scripts.Loader;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts
{
    public class FightigState : LifetimeScope
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
            _isValid = true;

        }
        private void Start()
        {
            if (!_isValid) SceneManager.LoadScene("Startup");
        }
    }
}