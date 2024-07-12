using System;
using _Project.Scripts.Character.Runtime.Data;
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
    [Inject] private FVXData _vfxData;
    private PlayerSM _playerSm;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private GameData _gameData;
    [Inject] private PlayerUIData _playerUIData;
    [Inject] private VolumeController _volumeController;
    private float _baseHealth = 50;
    private float _health = 50;
    // private float _baseHealth = 100f;
    // private float _health = 100f;
    private bool _isDead = false;

    //
    private float _crrTime = 0;
    private float _healingTimeRate = .5f;
    private float _healingAmount = 0.1f;

    public void Initialise(PlayerSM playerSM)
    {
      _playerSm = playerSM;
    }

    public void SetBaseHealt(float baseHealth)
    {
      _baseHealth = baseHealth;
      _health = baseHealth;
    }
    public void SetHealingAmount(float healingAmount)
    {
      _healingAmount = healingAmount;
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
      _volumeController.PlayChromatic();
      if (_health <= 0) Dead();
      SetHealtBar();
      DamageVFX();
    }

    private void DamageVFX()
    {
      if (_vfxData.GetDamageParticle != null) _vfxData.GetDamageParticle.Play();
    }

    private void SetHealtBar()
    {
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

    internal void ResetHealth()
    {
      _isDead = false;
      _health = _baseHealth;
      SetHealtBar();
    }
  }
}