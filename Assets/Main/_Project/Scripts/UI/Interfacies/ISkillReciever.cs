using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;

namespace _Project.Scripts.UI.Interfacies
{
    public interface ISkillReciever
    {    

        void OnSkillKill();
        void OnSkillPlacedInventory();
        void AbleToUpgrade();
        public void CloseBtnClicked();

        public void OnSkillBtnClicked(CreatedSkillInfo createdSkillInfo);
        void RerollBtnClicked();
    }
}