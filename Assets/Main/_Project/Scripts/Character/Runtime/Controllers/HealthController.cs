using System;
using UnityEngine;
using VContainer;

public class HealthController
{
  [Inject] private ICharacter _character;
  private float _health;
  private bool _isDead = false;
  public void GetDamage(float damage)
  {
    if (_isDead) return;
    _health -= damage;
    if (_health <= 0) Dead();
  }

  private void Dead()
  {
    Debug.LogWarning("Player dead");
  }
}