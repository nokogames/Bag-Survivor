using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Craft;
using UnityEngine;

public interface ICharacter
{
    public ITargetable Target { get; }
    public Transform Transform {get;}
    // GameObject Weapon { get; }
    // Transform ClosestEnemyBehaviour { get; }
    // BaseCharacterGraphics Graphics { get; }
    // bool IsCloseEnemyFound { get; }

    // public void SetTargetActive();
    // void OnGunShooted();
    // void SetTargetUnreachable();

    // public Transform transform { get; set; }
    // public float BulletDamageMultiplier { get; set; }
}
