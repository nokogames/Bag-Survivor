using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public IEnemy TargetEnemy { get; }
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
