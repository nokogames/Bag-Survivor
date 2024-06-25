using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Project.Scripts.UI.Interfacies
{
    public interface IUIMediatorEventHandler
    {
        public void AddReciever(ISkillReciever reciever);
        public void RemoveReciever(ISkillReciever reciever);
    }
}