using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Runtime.Controllers;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Scripts.Character.Runtime
{

    // public class PlayerController : MonoBehaviour, ICharacter, IEnemyDetector
    // {
    //     #region  Debug
    //     [Header("Debug")]
    //     public bool IsDetectedEnemy;
    //     #endregion
    //     [Inject] private PlayerMovementController _playerMovementController;



    //     [Inject] private CharacterGraphics _characterGraphics;

    //     [Inject] private BaseGunBehavior _gunBehavior;
    //     [Inject] private EnemyDetector _enemyDetector;
    //     // private MovementSettings _movementSettings;
    //     // private MovementSettings _activeMovementSettings;




       
    //     private bool _isCloseEnemyFound;

    //     // [Inject]
    //     // void InjectDependenciesAndInitialize(PlayerMovementController playerMovementController)
    //     // {
    //     //     _playerMovementController = playerMovementController;
    //     // }
    //     private void Awake()
    //     {
    //         //    _playerMovementController = new PlayerMovementController(inputData, characterGraphics, this);
    //         //Initializations();
    //     }
    //     private void Update()
    //     {
    //         if (_playerMovementController == null) return;
    //         // if (Input.GetKeyDown(KeyCode.W))
    //         // {
    //         //     _characterGraphics.DisableIK();
    //         // }
    //         _playerMovementController.Update();
    //         //Move();
    //         GunUpdate();
    //     }

    //     private void GunUpdate()
    //     {
    //         _gunBehavior.UpdateHandRig();
    //     }

    //     private void Initializations()
    //     {
    //         _playerMovementController.Initialise();
    //         // _movementSettings = characterGraphics.MovementSettings;
    //         //Change this when added aiming
    //         // _activeMovementSettings = characterGraphics.MovementSettings;
    //         _characterGraphics.Initialise(this);
    //         _gunBehavior.InitialiseCharacter(_characterGraphics, this);
    //         _enemyDetector.Initialise(this);
    //     }
    //     // private void Move()
    //     // {
    //     //     if (!inputData.IsValid && _isMoving) MovingStoped();

    //     //     if (!inputData.IsValid) return;

    //     //     if (!_isMoving) MovingStarted();

    //     //     float maxAlowedSpeed = Mathf.Clamp01(inputData.Magnitude) * _activeMovementSettings.MoveSpeed;
    //     //     _speed += _activeMovementSettings.Acceleration * Time.deltaTime;
    //     //     if (_speed > maxAlowedSpeed) _speed = maxAlowedSpeed;

    //     //     _movementVelocity = transform.forward * _speed;

    //     //     transform.position += inputData.MovementInput * Time.deltaTime * _speed;

    //     //     characterGraphics.OnMoving(Mathf.InverseLerp(0, _activeMovementSettings.MoveSpeed, _speed),
    //     //          inputData.MovementInput, _isCloseEnemyFound);
    //     //     //If not found enemy 
    //     //     Rotate();

    //     // }

    //     // private void Rotate()
    //     // {
    //     //     // if (!_isCloseEnemyFound) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(inputData.MovementInput), Time.deltaTime * _activeMovementSettings.RotationSpeed);
    //     //     // else
    //     //     // {
    //     //     //     Vector3 newTargetPoint = _targetEnemy.Transform.position;
    //     //     //     newTargetPoint.y = transform.position.y;
    //     //     //     Quaternion targetRotation = Quaternion.LookRotation(newTargetPoint - transform.position);
    //     //     //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _activeMovementSettings.RotationSpeed);
    //     //     //     transform.LookAt(new Vector3(_targetEnemy.Transform.position.x, transform.position.y, _targetEnemy.Transform.position.z));
    //     //     // }
    //     // }

    //     // private void MovingStoped()
    //     // {
    //     //  _playerMovementController.ISm   _isMoving = false;
    //     //     characterGraphics.OnMovingStoped();
    //     // }

    //     // private void MovingStarted()
    //     // {
    //     //     _speed = 0;
    //     //     _isMoving = true;
    //     //     characterGraphics.OnMovingStarted();
    //     // }



    //     public void OnEnemyDetected(IEnemy detectedEnemyInfo)
    //     {
    //         _targetEnemy = detectedEnemyInfo;
    //         // _isCloseEnemyFound = detectedEnemyInfo != null;

    //         _playerMovementController.TargetEnemy = detectedEnemyInfo;
    //         _playerMovementController.IsCloseEnemyFound = detectedEnemyInfo != null;
    //         // Debug.Log($"Enemy detected {detectedEnemyInfo}");
    //         // if (detectedEnemyInfo == null) return;
    //         // if (_targetEnemy != null) _targetEnemy.Transform.GetComponent<Renderer>().material.color = Color.red;
    //         // _targetEnemy = detectedEnemyInfo;
    //         // detectedEnemyInfo.Transform.GetComponent<Renderer>().material.color = Color.green;
    //     }
    // }

}