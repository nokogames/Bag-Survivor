using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    [CreateAssetMenu(fileName = "DamageSkill", menuName = "ScriptableObjects/SkillSystem/DamageSkill", order = 0)]
    public class DamageSkill : SkillBase
    {  

        public override void OnSelectedSkill()
        {
            Debug.Log("Selected Damage Skill");
        }
    }
}