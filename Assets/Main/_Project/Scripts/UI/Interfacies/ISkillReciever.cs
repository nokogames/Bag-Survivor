using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.SkillManagement.SO.Skills;

namespace _Project.Scripts.UI.Interfacies
{
    public interface ISkillReciever
    {
        void AbleToUpgrade();
        public void CloseBtnClicked();

        public void OnSkillBtnClicked(SkillBase skill);
        void RerollBtnClicked();
    }
}