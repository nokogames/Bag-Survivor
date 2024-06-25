using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

using _Project.Scripts.UI.Interfacies;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using _Project.Scripts.UI.Controllers;

namespace _Project.Scripts.SkillManager.Controllers
{

    public class SkillReciverController : ISkillReciever, IStartable, IDisposable
    {
        [Inject] private UIMediatorEventHandler _uiMediatorEventHandler;
        public void CloseBtnClicked()
        {

            Debug.Log($"Manger----CloseBtn");
        }


        public void Start()
        {
            _uiMediatorEventHandler.AddReciever(this);
        }
        public void Dispose()
        {
            _uiMediatorEventHandler.RemoveReciever(this);
        }
    }
}
