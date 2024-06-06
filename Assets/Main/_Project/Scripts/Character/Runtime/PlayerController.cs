using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter, IEnemyDetector
{
    #region  Debug
    [Header("Debug")]
    public bool IsDetectedEnemy;
    #endregion
    [SerializeField] private InputDataSO inputData;
    [SerializeField] private CharacterGraphics characterGraphics;

    [SerializeField] private BaseGunBehavior gunBehavior;
    [SerializeField] private EnemyDetector enemyDetector;
    private MovementSettings _movementSettings;
    private MovementSettings _activeMovementSettings;


    private bool _isMoving = false;
    private float _speed = 0;
    private Vector3 _movementVelocity;

    private IEnemy _targetEnemy;
    public IEnemy TargetEnemy => _targetEnemy;
    private bool _isCloseEnemyFound;
    private void Awake()
    {
        Initializations();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W  ))
        {
            characterGraphics.DisableIK();
        }
        Move();
        GunUpdate();
    }

    private void GunUpdate()
    {
        gunBehavior.UpdateHandRig();
    }

    private void Initializations()
    {
        _movementSettings = characterGraphics.MovementSettings;
        //Change this when added aiming
        _activeMovementSettings = characterGraphics.MovementSettings;
        characterGraphics.Initialise(this);
        gunBehavior.InitialiseCharacter(characterGraphics, this);
        enemyDetector.Initialise(this);
    }
    private void Move()
    {
        if (!inputData.IsValid && _isMoving) MovingStoped();

        if (!inputData.IsValid) return;

        if (!_isMoving) MovingStarted();

        float maxAlowedSpeed = Mathf.Clamp01(inputData.Magnitude) * _activeMovementSettings.MoveSpeed;
        _speed += _activeMovementSettings.Acceleration * Time.deltaTime;
        if (_speed > maxAlowedSpeed) _speed = maxAlowedSpeed;

        _movementVelocity = transform.forward * _speed;

        transform.position += inputData.MovementInput * Time.deltaTime * _speed;

        characterGraphics.OnMoving(Mathf.InverseLerp(0, _activeMovementSettings.MoveSpeed, _speed),
             inputData.MovementInput, _isCloseEnemyFound);
        //If not found enemy 
        Rotate();

    }

    private void Rotate()
    {
        if (!_isCloseEnemyFound) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(inputData.MovementInput), Time.deltaTime * _activeMovementSettings.RotationSpeed);
        else
        {
            Vector3 newTargetPoint = _targetEnemy.Transform.position;
            newTargetPoint.y = transform.position.y;
            Quaternion targetRotation = Quaternion.LookRotation(newTargetPoint - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _activeMovementSettings.RotationSpeed);
            transform.LookAt(new Vector3(_targetEnemy.Transform.position.x, transform.position.y, _targetEnemy.Transform.position.z));
        }
    }

    private void MovingStoped()
    {
        _isMoving = false;
        characterGraphics.OnMovingStoped();
    }

    private void MovingStarted()
    {
        _speed = 0;
        _isMoving = true;
        characterGraphics.OnMovingStarted();
    }



    public void OnEnemyDetected(IEnemy detectedEnemyInfo)
    {
        _targetEnemy = detectedEnemyInfo;
        _isCloseEnemyFound = detectedEnemyInfo != null;
        // Debug.Log($"Enemy detected {detectedEnemyInfo}");
        // if (detectedEnemyInfo == null) return;
        // if (_targetEnemy != null) _targetEnemy.Transform.GetComponent<Renderer>().material.color = Color.red;
        // _targetEnemy = detectedEnemyInfo;
        // detectedEnemyInfo.Transform.GetComponent<Renderer>().material.color = Color.green;
    }
}
