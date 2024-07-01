using System;
using _Project.Scripts.Character.Runtime.SerializeData;
using _Project.Scripts.Loader;
using _Project.Scripts.UI.Controllers;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Scripts.Character.Runtime.Controllers
{


  public class HealthController : IFixedTickable
  {
    private PlayerSM _playerSm;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private GameData _gameData;
    [Inject] private PlayerUIData _playerUIData;

    private float _baseHealth = 1;
    private float _health = 1;
    // private float _baseHealth = 100f;
    // private float _health = 100f;
    private bool _isDead = false;

    //
    private float _crrTime = 0;
    private float _healingTimeRate = .1f;
    private float _healingAmount = 0.1f;

    public void Initialise(PlayerSM playerSM)
    {
      _playerSm = playerSM;
    }
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
    
      if (_isDead) return;
      _health -= damage;

      if (_health <= 0) Dead();

      _playerUIData.BarFillAomunt = _health / _baseHealth;
    }

    private void Dead()
    {  
      _isDead = true;
      Debug.LogWarning("Player dead");

      _playerSm.ChangeState(_playerSm.DiedState);
      // string lvl = "Level" + (_gameData.CurrentLvl + 1).ToString();
      // _sceneLoader.LoadLevel(lvl);
    }
  }
}