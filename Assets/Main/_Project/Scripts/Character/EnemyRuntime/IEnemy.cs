
using _Project.Scripts.Character.Craft;
using UnityEngine;


public interface IEnemy : ITargetable
{
   
    public bool IsDead { get; set; }

    public void GetDamage(float damage);
    
}
