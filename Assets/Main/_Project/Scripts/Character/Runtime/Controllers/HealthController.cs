using System;
using _Project.Scripts.Character.Runtime.SerializeData;
using _Project.Scripts.Loader;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Scripts.Character.Runtime.Controllers
{


  public class HealthController : IFixedTickable
  {
    [Inject] private ICharacter _character;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private GameData _gameData;
    [Inject] private PlayerUIData _playerUIData;
    private float _baseHealth = 100f;
    private float _health = 100f;
    private bool _isDead = false;

    //
    private float _crrTime = 0;
    private float _healingTimeRate = .1f;
    private float _healingAmount = 1f;
    public void FixedTick()
    {

      // _playerUIData.BarFillAomunt = _health / _baseHealth;

      if (_health >= _baseHealth)
      {
        //   _playerUIData.EnabledBar = false;
        return;
      }

      _crrTime += Time.fixedDeltaTime;
      if (_crrTime < _healingTimeRate) return;
      _crrTime = 0;
      _health += _healingAmount;
      _playerUIData.EnabledBar = true;

    }

    public void GetDamage(float damage)
    {
      _playerUIData.EnabledBar = true;
      Debug.LogWarning($"Player damaged {damage}");
      if (_isDead) return;
      _health -= damage;

      if (_health <= 0) Dead();

      _playerUIData.BarFillAomunt = _health / _baseHealth;
    }

    private void Dead()
    {
      Debug.LogWarning("Player dead");
      string lvl = "Level" + (_gameData.CurrentLvl + 1).ToString();
      _sceneLoader.LoadLevel(lvl);
    }
  }
}