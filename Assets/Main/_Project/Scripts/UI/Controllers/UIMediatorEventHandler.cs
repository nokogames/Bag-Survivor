using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.UI.Interfacies;
using UnityEngine;

namespace _Project.Scripts.UI.Controllers
{

    public class UIMediatorEventHandler : MonoBehaviour,IUIMediatorEventHandler
    {
        private List<ISkillReciever> _recievers = new();
        public void AddReciever(ISkillReciever reciever)
        {
            if (!_recievers.Contains(reciever)) _recievers.Add(reciever);
        }
        public void RemoveReciever(ISkillReciever reciever)
        {
            if (_recievers.Contains(reciever)) _recievers.Remove(reciever);
        }
        public void CloseBtnClicked()
        {
            _recievers.ForEach(reciever => reciever.CloseBtnClicked());
        }
    }

}