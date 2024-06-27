using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Craft;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class PlayerMovementController
    {
        [Inject]
        private InputDataSO _inputData;
        [Inject]
        private CharacterGraphics _characterGraphics;
        [Inject] private Transform _playerTransform;
        public bool IsCloseEnemyFound
        {
            get => _isCloseEnemyFound; set
            {
                _isCloseEnemyFound = value;
                SetMovementSettings();
            }
        }



        public MovementSettings ActiveMovementSettings { set => _activeMovementSettings = value; }
        public ITargetable Target { get => _target; set => _target = value; }

        public bool IsMoving { get => _isMoving; set => _isMoving = value; }

        private ITargetable _target;
        private MovementSettings _activeMovementSettings;
        private bool _isCloseEnemyFound = false;
        private bool _isMoving = false;
        private float _speed = 0;
        private Vector3 _movementVelocity;



        public void Initialise()
        {
            _activeMovementSettings = _characterGraphics.MovementSettings;

        }
        private void Move()
        {
            Rotate();
            if (!_inputData.IsValid && _isMoving) MovingStoped();

            if (!_inputData.IsValid) return;

            if (!_isMoving) MovingStarted();

            float maxAlowedSpeed = Mathf.Clamp01(_inputData.Magnitude) * _activeMovementSettings.MoveSpeed;
            _speed += _activeMovementSettings.Acceleration * Time.deltaTime;
            if (_speed > maxAlowedSpeed) _speed = maxAlowedSpeed;

            _movementVelocity = _playerTransform.forward * _speed;

            _playerTransform.position += _inputData.MovementInput * Time.deltaTime * _speed;

            _characterGraphics.OnMoving(Mathf.InverseLerp(0, _activeMovementSettings.MoveSpeed, _speed),
                 _inputData.MovementInput, _isCloseEnemyFound);
            //If not found enemy 
            //  Rotate();

        }
        public void Update()
        {
            Move();

        }

        private void MovingStoped()
        {
            _isMoving = false;
            _characterGraphics.OnMovingStoped();
        }

        private void Rotate()
        {
            if (_inputData.MovementInput == Vector3.zero) return;

            if (!_isCloseEnemyFound) _playerTransform.rotation =
             Quaternion.Lerp(_playerTransform.rotation, Quaternion.LookRotation(_inputData.MovementInput), Time.deltaTime * _activeMovementSettings.RotationSpeed);
            else
            {
                Vector3 newTargetPoint = _target.Transform.position;
                newTargetPoint.y = _playerTransform.position.y;
                Quaternion targetRotation = Quaternion.LookRotation(newTargetPoint - _playerTransform.position);
                _playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, targetRotation, Time.deltaTime * _activeMovementSettings.RotationSpeed);
                //   _playerTransform.LookAt(new Vector3(_targetEnemy.Transform.position.x, _playerTransform.position.y, _targetEnemy.Transform.position.z));
            }
        }

        private void MovingStarted()
        {
            _speed = 0;
            _isMoving = true;
            _characterGraphics.OnMovingStarted();
        }

        private void SetMovementSettings()
        {
            _activeMovementSettings = IsCloseEnemyFound ? _characterGraphics.MovementAimingSettings : _characterGraphics.MovementSettings;

        }
    }
}