using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI.Interfacies;
using UnityEngine;

namespace _Project.Scripts.UI.Controllers
{

    public class UIMediatorEventHandler : MonoBehaviour, IUIMediatorEventHandler
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
        public void RerollBtnClicked()
        {
            Debug.Log("Reroll from Event handler");
            _recievers.ForEach(reciever => reciever.RerollBtnClicked());
        }

        public void OnSkillBtnClicked(SkillBase skill)
        {
            _recievers.ForEach(reciever => reciever.OnSkillBtnClicked(skill));
        }
    }

}